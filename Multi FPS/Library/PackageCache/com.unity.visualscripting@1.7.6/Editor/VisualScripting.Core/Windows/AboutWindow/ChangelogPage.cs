using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Unity.VisualScripting
{
    public class ChangelogPage : Page
    {
        public ChangelogPage(PluginChangelog changelog, bool showPluginName)
        {
            if (showPluginName)
            {
                title = shortTitle = changelog.plugin.manifest.name;
                subtitle = $"v.{changelog.version}";
            }
            else
            {
                title = subtitle = $"Versi