using Cinar.Database;

namespace Cinar.CMS.DesktopEditor.Entities
{
    public class Tag : NamedEntity
    {
        private string displayName;
        public string DisplayName
        {
            get { return displayName; }
            set { displayName = value; }
        }

        private bool headline = false;
        public bool Headline
        {
            get { return headline; }
            set { headline = value; }
        }

        private int contentCount = 0;
        public int ContentCount
        {
            get { return contentCount; }
            set { contentCount = value; }
        }

        private bool noise = false;
        public bool Noise
        {
            get { return noise; }
            set { noise = value; }
        }

    }
}
