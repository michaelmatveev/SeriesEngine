﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SeriesEngine.ExcelAddIn.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SeriesEngine.ExcelAddIn.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot; ?&gt;
        ///&lt;ObjectGrid xmlns=&quot;http://www.seriesengine.com/SeriesEngine.ExcelAddIn/GridFragments&quot; Version=&quot;1&quot; Name=&quot;Таблица1&quot; Sheet=&quot;Лист1&quot; Cell=&quot;C2&quot;&gt;
        ///  &lt;CFragment Caption=&quot;Регион&quot; CollectionName=&quot;Main&quot; Level=&quot;1&quot; RefObject=&quot;Region&quot; Type=&quot;UniqueName&quot;/&gt;
        ///  &lt;CFragment Caption=&quot;Потребитель&quot; CollectionName=&quot;Main&quot; Level=&quot;2&quot; RefObject=&quot;Customer&quot; Type=&quot;UniqueName&quot;/&gt;
        ///  &lt;CFragment Caption=&quot;Договор&quot; CollectionName=&quot;Main&quot; Level=&quot;3&quot; RefObject=&quot;Contract&quot; Type=&quot;UniqueName&quot;/&gt;
        ///  &lt;VFragment Ca [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string TestGrid {
            get {
                return ResourceManager.GetString("TestGrid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot; ?&gt;
        ///&lt;DataToImport&gt;
        ///  &lt;!-- Данные импортируются с максимального уровня--&gt;
        ///  &lt;Region UniqueName=&quot;Пензенская область&quot;&gt;
        ///    &lt;Customer UniqueName=&quot;ООО &amp;quot;МагнитЭнерго&amp;quot;&quot;&gt;
        ///      &lt;Contract UniqueName=&quot;1001014-ЭН&quot; Since=&quot;2002-05-30T09:00:00&quot; Till=&quot;2008-05-30T09:00:00&quot;&gt;
        ///        &lt;ContractType&gt;КП&lt;/ContractType&gt;
        ///        &lt;ConsumerObject UniqueName=&quot;ММ &amp;quot;Влад&amp;quot; г. Пенза пр-т. Строителей, 24а&quot;&gt;
        ///          &lt;Point UniqueName=&quot;ТП-530&quot;&gt;
        ///            &lt;VoltageLevel&gt;СН-2&lt; [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string TestGridData {
            get {
                return ResourceManager.GetString("TestGridData", resourceCulture);
            }
        }
    }
}
