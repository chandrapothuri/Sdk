﻿using EdiFabric.Core.Model.Edi.X12;
using EdiFabric.Framework;
using EdiFabric.Rules.X12_004010;
using System.Reflection;

namespace EdiFabric.Sdk.TemplateFactories
{
    public class X12Factories
    {
        /// <summary>
        /// Parse the transaction explicitly.
        /// </summary>
        /// <param name="isa">The ISA.</param>
        /// <param name="gs">The GS.</param>
        /// <param name="st">The ST.</param>
        /// <returns>The type to parse to.</returns>
        public static TypeInfo FullTemplateFactory(ISA isa, GS gs, ST st)
        {
            if (isa.InterchangeSenderID_6 == "SPLIT1" &&
               st.TransactionSetIdentifierCode_01 == "850")
                return typeof(TS850Split).GetTypeInfo();

            if (isa.InterchangeSenderID_6 == "INVALID1" &&
                st.TransactionSetIdentifierCode_01 == "850")
                return typeof(TS850Validation).GetTypeInfo();

            if (isa.InterchangeSenderID_6 == "CUSTOM11" &&
                st.TransactionSetIdentifierCode_01 == "850")
                return typeof(TS850Custom1).GetTypeInfo();

            if (isa.InterchangeSenderID_6 == "CUSTOM12" &&
               st.TransactionSetIdentifierCode_01 == "850")
                return typeof(TS850Custom2).GetTypeInfo();

            if (gs.VersionAndRelease_8 == "004010" &&
                 st.TransactionSetIdentifierCode_01 == "850")
                return typeof(TS850).GetTypeInfo();

            if (gs.VersionAndRelease_8 == "004010" &&
                  st.TransactionSetIdentifierCode_01 == "810")
                return typeof(TS810).GetTypeInfo();

            throw new System.Exception(string.Format("Transaction {0} for version {1} is not supported.",
                st.TransactionSetIdentifierCode_01, gs.VersionAndRelease_8));
        }
        
        public static Assembly TrialTemplateFactory(MessageContext messageContext)
        {
            if (messageContext.Version == "004010")
                return Assembly.Load("EdiFabric.Rules.X12004010");

            throw new System.Exception(string.Format("Unsupported version {0}", messageContext.Version));
        }
    }
}
