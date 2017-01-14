internal static class Constants
{
    internal class Relationships
    {
        internal const string FollowRelationship = "FOLLOW";
        internal const string GoingRelationship = "GOING";
        internal const string ManageRelationship = "MANAGE";
        internal const string PlaceRelationship = "PLACE";

        public bool validate(string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;

            switch (value)
            {
                case FollowRelationship:
                    return true;
                case GoingRelationship:
                    return true;
                case ManageRelationship:
                    return true;
                case PlaceRelationship:
                    return true;
                default: return false;
            }
        }
    }

    internal class EntityNodeLabels
    {
        internal const string tmpLabel = "tmp";

        public bool validate(string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;

            switch (value)
            {
                default: return false;
            }
        }
    }

}