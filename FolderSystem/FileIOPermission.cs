namespace FolderSystem
{
    internal class FileIOPermission
    {
        private object pathDiscovery;
        private string rootDirectory;

        public FileIOPermission(object pathDiscovery, string rootDirectory)
        {
            this.pathDiscovery = pathDiscovery;
            this.rootDirectory = rootDirectory;
        }
    }
}