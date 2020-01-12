using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using Azure.Data.AppConfiguration;

namespace appcfgpwsh
{
    [Cmdlet(VerbsDiagnostic.Test,"SampleCmdlet")]
    [OutputType(typeof(FavoriteStuff))]
    public class TestSampleCmdletCommand : PSCmdlet
    {
        [Parameter(
            Mandatory = true,
            Position = 0,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true)]
        public int FavoriteNumber { get; set; }

        [Parameter(
            Position = 1,
            ValueFromPipelineByPropertyName = true)]
        [ValidateSet("Cat", "Dog", "Horse")]
        public string FavoritePet { get; set; } = "Dog";

        // This method gets called once for each cmdlet in the pipeline when the pipeline starts executing
        protected override void BeginProcessing()
        {
            WriteVerbose("Begin!");
        }

        // This method will be called for each input received from the pipeline to this cmdlet; if no input is received, this method is not called
        protected override void ProcessRecord()
        {
           string connStr = "Endpoint=https://appcfgtest01.azconfig.io;Id=rZyK-l4-s0:YDHCMKKMxPwaKSSsRMgA;Secret=LhVYdohgAjtCk48WgMejBpSC2JN4diubdbCySPTK5Mk=";

            var client = new ConfigurationClient(connStr);
            var settingToCreate = new ConfigurationSetting("some_key_pwsh", "some_value");
            ConfigurationSetting setting = client.SetConfigurationSetting(settingToCreate);

        }

        // This method will be called once at the end of pipeline execution; if no input is received, this method is not called
        protected override void EndProcessing()
        {
            WriteVerbose("End!");
        }
    }

    public class FavoriteStuff
    {
        public int FavoriteNumber { get; set; }
        public string FavoritePet { get; set; }
    }
}
