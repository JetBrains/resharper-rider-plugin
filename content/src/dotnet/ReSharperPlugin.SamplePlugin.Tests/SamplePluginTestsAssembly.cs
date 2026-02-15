using JetBrains.TestFramework;
using NUnit.Framework;

namespace ReSharperPlugin.SamplePlugin.Tests;

[SetUpFixture]
public class SamplePluginTestsAssembly : ExtensionTestEnvironmentAssembly<SamplePluginTestEnvironmentZone>;
