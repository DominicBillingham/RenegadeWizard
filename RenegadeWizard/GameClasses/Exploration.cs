
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





        }

    }
}
