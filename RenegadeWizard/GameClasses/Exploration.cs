
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
                    "The pleasant sunlight flickers through the oak leaves, small creatures dart through the undergrowths. <WayfarerForest>" +
                    "\n # a small stream wanders through the mud, seemingly making it's own path through the woodland. <FisherCreek>";
            }





        }

    }
}
