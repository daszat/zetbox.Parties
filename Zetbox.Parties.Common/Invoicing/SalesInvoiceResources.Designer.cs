﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.296
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Zetbox.Parties.Common.Invoicing {
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
    internal class SalesInvoiceResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal SalesInvoiceResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Zetbox.Parties.Common.Invoicing.SalesInvoiceResources", typeof(SalesInvoiceResources).Assembly);
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
        ///   Looks up a localized string similar to This invoice is already canceled..
        /// </summary>
        internal static string IsAlreadyCanceled {
            get {
                return ResourceManager.GetString("IsAlreadyCanceled", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invoice is already finalized..
        /// </summary>
        internal static string IsAlreadyFinalized {
            get {
                return ResourceManager.GetString("IsAlreadyFinalized", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Only finalized invoices have to be canceled..
        /// </summary>
        internal static string IsNotFinalized {
            get {
                return ResourceManager.GetString("IsNotFinalized", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Only saved and unmodified invoices can be finalized..
        /// </summary>
        internal static string IsNotSaved {
            get {
                return ResourceManager.GetString("IsNotSaved", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Reversal invoices cannot be canceled..
        /// </summary>
        internal static string IsReversal {
            get {
                return ResourceManager.GetString("IsReversal", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Internal Organization has no invoice generator. Unable to create invoice id..
        /// </summary>
        internal static string NoInvoiceGenerator {
            get {
                return ResourceManager.GetString("NoInvoiceGenerator", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No Internal Organization was selected..
        /// </summary>
        internal static string NoOrgSelected {
            get {
                return ResourceManager.GetString("NoOrgSelected", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Reversal of invoice {0}.
        /// </summary>
        internal static string ReversalDescriptionFmt {
            get {
                return ResourceManager.GetString("ReversalDescriptionFmt", resourceCulture);
            }
        }
    }
}