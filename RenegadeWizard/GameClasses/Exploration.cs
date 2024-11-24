
using RenegadeWizard.Components;
using RenegadeWizard.Entities.Creatures;
using RenegadeWizard.Entities.Creatures.Human;
using RenegadeWizard.Entities.Creatures.Misc;

namespace RenegadeWizard.GameClasses
{
    static public class WorldNavigation
    {
        static public List<string> LocationsFound { get; set; } = new();

        static public void TravelTo(string location)
        {

            if (location == null)
            {
                return;
            }


            if ( "wayfarerforest".Contains(location) )
            {
                Console.WriteLine(" # You arrive at Wayfarer Forest");

                Scene.ResetScene();
                Scene.Description =
                    "The pleasant sunlight flickers through the birch trees of <WayfarerWoods>, leaving the autumn leaves speckled with light." +
                    " A small creek wanders through the mud, it's waters emboldened by the morning rain as it trails towards <FisherCreek>." +
                    " Some form of monster has scarred the landscape leaving upturned mud and leaf in it's wake. " +
                    " As if it was the aftermath of a violent river, the destruction winds and bends before entering <TheSlitheringHalls> "; 
            }

            if ( "fishercreek".Contains(location) )
            {
                Console.WriteLine(" # You arrive at Fisher Creek");

                Scene.ResetScene();
                Scene.Description =
                    "After following the creek, the water coalesces into a crescent lake. Peering into the suprisingly clear water, you see a blend of colours." +
                    " An endless river of koi fish create their own stream not of water, but colour below the surface. Some are colossal, but they glide all the same.";

                var pete = new Human("FishermanPete");
                pete.Conversation = new FishermanConversation();

                Scene.Entities.Add(pete);
            }

            if ("slitheringhalls".Contains(location))
            {
                Console.WriteLine(" # You arrive at the slithering halls");

                Scene.ResetScene();
                Scene.Description =
                    " Colossal tunnels have been carved from the rock, " +
                    " An endless river of koi fish create their own stream not of water, but colour below the surface. Some are colossal, but they glide all the same.";
            }



        }

    }
}
