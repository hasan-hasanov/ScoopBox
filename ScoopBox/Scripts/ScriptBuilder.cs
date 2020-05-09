﻿using ScoopBox.Scripts.Abstract;
using System.Text;

namespace ScoopBox.Scripts
{
    public class ScriptBuilder : IScriptBuilder
    {
        private readonly ISetExecutionPolicy setExecutionPolicy;
        private readonly IScoopInstaller scoopInstaller;
        private readonly IScoopBuckets scoopBuckets;
        private readonly IAppInstaller appInstaller;

        public ScriptBuilder(
            ISetExecutionPolicy setExecutionPolicy,
            IScoopInstaller scoopInstaller,
            IScoopBuckets scoopBuckets,
            IAppInstaller appInstaller)
        {
            this.setExecutionPolicy = setExecutionPolicy;
            this.scoopInstaller = scoopInstaller;
            this.scoopBuckets = scoopBuckets;
            this.appInstaller = appInstaller;
        }

        public string Build(ScoopBoxOptions scoopBoxOptions)
        {
            StringBuilder installerBuilder = new StringBuilder();

            installerBuilder.AppendLine(this.setExecutionPolicy.Set());
            installerBuilder.AppendLine(this.scoopInstaller.Install());
            installerBuilder.AppendLine(this.scoopBuckets.Add("extras"));
            installerBuilder.AppendLine(this.appInstaller.Install(scoopBoxOptions.Apps));

            return installerBuilder.ToString();
        }
    }
}