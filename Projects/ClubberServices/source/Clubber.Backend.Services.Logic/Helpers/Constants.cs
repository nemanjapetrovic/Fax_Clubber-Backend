namespace Clubber.Backend.Services.Logic.Helpers
{
    internal static class Constants
    {
        internal class RedisDB
        {
            internal static string ClubEntityName = "club";
            internal static string EventEntityName = "event";
            internal static string AdditionalInfoName = "name";
            internal static string AdditionalInfoId = "id";
        }

        internal class Neo4jDBRelationships
        {
            internal const string FollowRelationship = "FOLLOW";
            internal const string GoingRelationship = "GOING";
            internal const string ManageRelationship = "MANAGE";
            internal const string PlaceRelationship = "PLACE";
        }

        internal class Neo4jDBNodeLabels
        {
            internal const string LabelNodeUser = "User";
            internal const string LabelNodeManager = "Manager";
            internal const string LabelNodeEvent = "Event";
            internal const string LabelNodeClub = "Club";

        }
    }
}
