using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Unity.VisualScripting
{
    public class ConfigurationPanel
    {
        public ConfigurationPanel(Product product)
        {
            Ensure.That(nameof(product)).IsNotNull(product);

            this.product = product;
            configurations = product.plugins.Select(plugin => plugin.configuration).ToList();