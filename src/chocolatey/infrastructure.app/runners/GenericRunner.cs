﻿// Copyright © 2011 - Present RealDimensions Software, LLC
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// 
// You may obtain a copy of the License at
// 
// 	http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace chocolatey.infrastructure.app.runners
{
    using System;
    using System.Linq;
    using SimpleInjector;
    using adapters;
    using attributes;
    using commandline;
    using configuration;
    using infrastructure.commands;
    using logging;
    using Console = System.Console;
    using Environment = System.Environment;

    public sealed class GenericRunner
    {
        public void run(ChocolateyConfiguration config, Container container, bool isConsole, Action<ICommand> parseArgs)
        {
            var commands = container.GetAllInstances<ICommand>();
            var command = commands.Where((c) =>
                {
                    var attributes = c.GetType().GetCustomAttributes(typeof (CommandForAttribute), false);
                    return attributes.Cast<CommandForAttribute>().Any(attribute => attribute.CommandName.is_equal_to(config.CommandName));
                }).FirstOrDefault();

            if (command == null)
            {
                //todo add a search among other location/extensions for the command
                if (!string.IsNullOrWhiteSpace(config.CommandName))
                {
                    throw new Exception("Could not find a command registered that meets '{0}'".format_with(config.CommandName));
                }

                if (isConsole) Environment.ExitCode = 1;
            }
            else
            {
                if (command.may_require_admin_access())
                {
                    warn_when_admin_needs_elevation(config);
                }

                if (parseArgs != null)
                {
                    parseArgs.Invoke(command);
                }

                this.Log().Debug(() => "Configuration: {0}".format_with(config.ToString()));


                if (isConsole && config.HelpRequested)
                {
#if DEBUG
                    Console.WriteLine("Press enter to continue...");
                    Console.ReadKey();
#endif
                    Environment.Exit(1);
                }

                var token = Assembly.GetExecutingAssembly().get_public_key_token();
                if (string.IsNullOrWhiteSpace(token) || token != ApplicationParameters.OfficialChocolateyPublicKey)
                {
                    if (!config.AllowUnofficialBuild)
                    {
                        throw new Exception(@"
Custom unofficial builds are not allowed by default.
 To override this behavior, explicitly set --allow-unofficial.
 See the help menu (choco -h) for options.");
                    }
                    else
                    {
                        this.Log().Warn(ChocolateyLoggers.Important, @"
Chocolatey is not an official build (bypassed with --allow-unofficial).
 If you are seeing this message and it is not expected, your system may 
 now be in a bad state. Only official builds are to be trusted.
"
                         );

                    }
                }

                if (config.Noop)
                {
                    if (config.RegularOutput)
                    {
                        this.Log().Info("_ {0}:{1} - Noop Mode _".format_with(ApplicationParameters.Name, command.GetType().Name));
                    }

                    command.noop(config);
                }
                else
                {
                    this.Log().Debug("_ {0}:{1} - Normal Run Mode _".format_with(ApplicationParameters.Name, command.GetType().Name));
                    command.run(config);
                }
            }
        }

        public void warn_when_admin_needs_elevation(ChocolateyConfiguration config)
        {
            var shouldWarn = (!config.Information.IsProcessElevated && config.Information.IsUserAdministrator);

            if (shouldWarn)
            {
                this.Log().Warn(ChocolateyLoggers.Important, @"Chocolatey detected you are not running from an elevated command shell
 (cmd/powershell).");
            }

            // NOTE: blended options may not have been fully initialized yet
            if (!config.PromptForConfirmation) return;

            if (shouldWarn)
            {
                var selection = InteractivePrompt.prompt_for_confirmation(@"
 You may experience errors - many functions/packages
 require admin rights. Only advanced users should run choco w/out an
 elevated shell. When you open the command shell, you should ensure 
 that you do so with ""Run as Administrator"" selected.

 Do you want to continue?", new[] { "yes", "no" }, defaultChoice: null, requireAnswer: true);

                if (selection.is_equal_to("no"))
                {
                    Environment.Exit(-1);
                }
            }
        }

    }
}