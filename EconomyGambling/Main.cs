using System;
using System.Collections.Generic;
using Rocket.API.Commands;
using Rocket.API.DependencyInjection;
using Rocket.API.Eventing;
using Rocket.Core.Eventing;
using Rocket.Core.Player.Events;
using Rocket.Core.Plugins;
using Rocket.Core.User;
using Rocket.Core.Player;
using fr34kyn01535.Uconomy;
using Rocket.Core.I18N;

namespace EconomyGambling
{
    public class Main : Plugin<Config>
    {
        public Main(IDependencyContainer container) : base("Welcome Messager", container)
        {

        }
        protected override void OnLoad(bool isFromReload)
        {
            Logger.Log("[EconomyGambling] Made by: Jonnyfencing1");
            Logger.Log("[EconomyGambling] Check out my personal plugin website https://tridentplugins.me");
            Logger.Log("----------------------------------------------");
            Logger.Log("[EconomyGambling] Finished Loading!");
        }
    }
    public class DiceRoll : ICommand
    {
        private Config config;
        public string Name => "diceroll";
        public string[] Aliases => null;
        public string Summary => "";
        public string Description => null;
        public string Permission => "tri.diceroll";
        public string Syntax => "<Bet Amount>";
        public IChildCommand[] ChildCommands => null;

        public bool SupportsUser(Type _)
        {
            return true;
        }

        public void Execute(ICommandContext context)
        {
            //Checking for player input.
            
            if (context.Parameters != null && context.Parameters.Length > 0)
            {

                //Ensuring player input is valid.
                if (float.TryParse(context.Parameters[0], out float x))
                {

                    //Ensuring the input is positive and not zero.
                    if (x <= 0)
                    {
                    
            if (context.Parameters != null && context.Parameters.Length > 0) {

                //Ensuring player input is valid.
                if (float.TryParse(context.Parameters[0], out float y)) {

                    //Ensuring the input is positive and not zero.
                    if (y <= 0) {

                        //Getting the players balance.
                        decimal balance = Uconomy.Instance.Database.GetBalance(context.User.Id);

                        //Ensuring the player has enough money to make the gamble.
                        if (balance >= (decimal)y)
                        {
                        if (balance >= (decimal)y) {

                            //Getting the random role.
                            Random rnd = new Random();
                            int rollresult = rnd.Next(1, 7);

                            //Getting the multiplier for that role.
                            float multiplier = (float)config.GetType().GetField("roll" + rollresult + "Multiplier").GetValue(config);

                            //Getting the amount to increase by. This accounts for the money subtracted for the original gamble.
                            float newAmount = (y * multiplier) - y;

                            //Increasing the players balance.
                            Uconomy.Instance.Database.IncreaseBalance(context.User.Id, (decimal)newAmount);

                            //Give the player some information about the transaction.
                            context.User.SendMessage("You rolled a " + rollresult + (float)balance + newAmount);

                        }
                        else context.User.SendMessage("You don't have that much money!");

                    }
                    else context.User.SendMessage("Please specify a positive and non-zero number!");

                }
                else context.User.SendMessage("Please specify a real number!");

            }
            else context.User.SendMessage("Please specify an argument!");
            
                        } else context.User.SendMessage("You don't have that much money!");
                        
                    } else context.User.SendMessage("Please specify a positive and non-zero number!");
                    
                } else context.User.SendMessage("Please specify a real number!");
                
            } else context.User.SendMessage("Please specify an argument!");

        }
    }
    public class Config
    {
        public float roll1Multiplier = 0;
        public float roll2Multiplier = 0.75f;
        public float roll3Multiplier = 0.5f;
        public float roll4Multiplier = 1;
        public float roll5Multiplier = 3;
        public float roll6Multiplier = 4;
    }
    public class EventListener : IEventListener<PlayerChatEvent>
    { 

        public EventListener(Config config)
        {
        }
        public void HandleEvent(IEventEmitter emitter, PlayerChatEvent @event)
        {

        }
    }
}
