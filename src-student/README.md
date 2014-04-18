Student Training Project
========
This is a very basic Visual Studio project that's used by students to complete exercises during Active Commerce training. It is set up to have the needed assembly references for working with Active Commerce, and is also configured to copy build results into the web root, so that the training work can be done outside the IIS website root.

Before opening it, open "deploy.targets" and edit the path to match your Sitecore website root. After opening, run a build. You can verify it's working by confirming that "test.js" now appears under your Sitecore web root.