namespace yoma;

public class ApplicationInstaller
{
    public static ApplicationInstaller instance;
    
    public ApplicationInstaller CreateApplicationInstaller()
    {
        ApplicationInstaller installer = new ApplicationInstaller();
        instance = installer;
        return installer;
    }

    public void InstallApplication()
    {
        
    }
}