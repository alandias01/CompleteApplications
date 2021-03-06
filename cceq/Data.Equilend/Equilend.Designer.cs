﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.EntityClient;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;

[assembly: EdmSchemaAttribute()]

namespace Data.Equilend
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class EquilendEntities : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new EquilendEntities object using the connection string found in the 'EquilendEntities' section of the application configuration file.
        /// </summary>
        public EquilendEntities() : base("name=EquilendEntities", "EquilendEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new EquilendEntities object.
        /// </summary>
        public EquilendEntities(string connectionString) : base(connectionString, "EquilendEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new EquilendEntities object.
        /// </summary>
        public EquilendEntities(EntityConnection connection) : base(connection, "EquilendEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<DTCMap> DTCMaps
        {
            get
            {
                if ((_DTCMaps == null))
                {
                    _DTCMaps = base.CreateObjectSet<DTCMap>("DTCMaps");
                }
                return _DTCMaps;
            }
        }
        private ObjectSet<DTCMap> _DTCMaps;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<LegalEntityContact> LegalEntityContacts
        {
            get
            {
                if ((_LegalEntityContacts == null))
                {
                    _LegalEntityContacts = base.CreateObjectSet<LegalEntityContact>("LegalEntityContacts");
                }
                return _LegalEntityContacts;
            }
        }
        private ObjectSet<LegalEntityContact> _LegalEntityContacts;

        #endregion
        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the DTCMaps EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToDTCMaps(DTCMap dTCMap)
        {
            base.AddObject("DTCMaps", dTCMap);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the LegalEntityContacts EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToLegalEntityContacts(LegalEntityContact legalEntityContact)
        {
            base.AddObject("LegalEntityContacts", legalEntityContact);
        }

        #endregion
    }
    

    #endregion
    
    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="EquilendModel", Name="DTCMap")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class DTCMap : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new DTCMap object.
        /// </summary>
        /// <param name="dTCMapId">Initial value of the DTCMapId property.</param>
        /// <param name="dTC">Initial value of the DTC property.</param>
        /// <param name="legalEntity">Initial value of the LegalEntity property.</param>
        public static DTCMap CreateDTCMap(global::System.Int32 dTCMapId, global::System.String dTC, global::System.String legalEntity)
        {
            DTCMap dTCMap = new DTCMap();
            dTCMap.DTCMapId = dTCMapId;
            dTCMap.DTC = dTC;
            dTCMap.LegalEntity = legalEntity;
            return dTCMap;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 DTCMapId
        {
            get
            {
                return _DTCMapId;
            }
            set
            {
                if (_DTCMapId != value)
                {
                    OnDTCMapIdChanging(value);
                    ReportPropertyChanging("DTCMapId");
                    _DTCMapId = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("DTCMapId");
                    OnDTCMapIdChanged();
                }
            }
        }
        private global::System.Int32 _DTCMapId;
        partial void OnDTCMapIdChanging(global::System.Int32 value);
        partial void OnDTCMapIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String DTC
        {
            get
            {
                return _DTC;
            }
            set
            {
                OnDTCChanging(value);
                ReportPropertyChanging("DTC");
                _DTC = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("DTC");
                OnDTCChanged();
            }
        }
        private global::System.String _DTC;
        partial void OnDTCChanging(global::System.String value);
        partial void OnDTCChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String LegalEntity
        {
            get
            {
                return _LegalEntity;
            }
            set
            {
                OnLegalEntityChanging(value);
                ReportPropertyChanging("LegalEntity");
                _LegalEntity = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("LegalEntity");
                OnLegalEntityChanged();
            }
        }
        private global::System.String _LegalEntity;
        partial void OnLegalEntityChanging(global::System.String value);
        partial void OnLegalEntityChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.DateTime> DateEntered
        {
            get
            {
                return _DateEntered;
            }
            set
            {
                OnDateEnteredChanging(value);
                ReportPropertyChanging("DateEntered");
                _DateEntered = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("DateEntered");
                OnDateEnteredChanged();
            }
        }
        private Nullable<global::System.DateTime> _DateEntered;
        partial void OnDateEnteredChanging(Nullable<global::System.DateTime> value);
        partial void OnDateEnteredChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String EnteredBy
        {
            get
            {
                return _EnteredBy;
            }
            set
            {
                OnEnteredByChanging(value);
                ReportPropertyChanging("EnteredBy");
                _EnteredBy = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("EnteredBy");
                OnEnteredByChanged();
            }
        }
        private global::System.String _EnteredBy;
        partial void OnEnteredByChanging(global::System.String value);
        partial void OnEnteredByChanged();

        #endregion
    
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="EquilendModel", Name="LegalEntityContact")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class LegalEntityContact : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new LegalEntityContact object.
        /// </summary>
        /// <param name="legalEntityContactId">Initial value of the LegalEntityContactId property.</param>
        /// <param name="dTCMapId">Initial value of the DTCMapId property.</param>
        public static LegalEntityContact CreateLegalEntityContact(global::System.Int32 legalEntityContactId, global::System.Int32 dTCMapId)
        {
            LegalEntityContact legalEntityContact = new LegalEntityContact();
            legalEntityContact.LegalEntityContactId = legalEntityContactId;
            legalEntityContact.DTCMapId = dTCMapId;
            return legalEntityContact;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 LegalEntityContactId
        {
            get
            {
                return _LegalEntityContactId;
            }
            set
            {
                if (_LegalEntityContactId != value)
                {
                    OnLegalEntityContactIdChanging(value);
                    ReportPropertyChanging("LegalEntityContactId");
                    _LegalEntityContactId = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("LegalEntityContactId");
                    OnLegalEntityContactIdChanged();
                }
            }
        }
        private global::System.Int32 _LegalEntityContactId;
        partial void OnLegalEntityContactIdChanging(global::System.Int32 value);
        partial void OnLegalEntityContactIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 DTCMapId
        {
            get
            {
                return _DTCMapId;
            }
            set
            {
                OnDTCMapIdChanging(value);
                ReportPropertyChanging("DTCMapId");
                _DTCMapId = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("DTCMapId");
                OnDTCMapIdChanged();
            }
        }
        private global::System.Int32 _DTCMapId;
        partial void OnDTCMapIdChanging(global::System.Int32 value);
        partial void OnDTCMapIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String LegalEntity
        {
            get
            {
                return _LegalEntity;
            }
            set
            {
                OnLegalEntityChanging(value);
                ReportPropertyChanging("LegalEntity");
                _LegalEntity = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("LegalEntity");
                OnLegalEntityChanged();
            }
        }
        private global::System.String _LegalEntity;
        partial void OnLegalEntityChanging(global::System.String value);
        partial void OnLegalEntityChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                OnNameChanging(value);
                ReportPropertyChanging("Name");
                _Name = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Name");
                OnNameChanged();
            }
        }
        private global::System.String _Name;
        partial void OnNameChanging(global::System.String value);
        partial void OnNameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Role
        {
            get
            {
                return _Role;
            }
            set
            {
                OnRoleChanging(value);
                ReportPropertyChanging("Role");
                _Role = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Role");
                OnRoleChanged();
            }
        }
        private global::System.String _Role;
        partial void OnRoleChanging(global::System.String value);
        partial void OnRoleChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Email
        {
            get
            {
                return _Email;
            }
            set
            {
                OnEmailChanging(value);
                ReportPropertyChanging("Email");
                _Email = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Email");
                OnEmailChanged();
            }
        }
        private global::System.String _Email;
        partial void OnEmailChanging(global::System.String value);
        partial void OnEmailChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String PhoneNumber
        {
            get
            {
                return _PhoneNumber;
            }
            set
            {
                OnPhoneNumberChanging(value);
                ReportPropertyChanging("PhoneNumber");
                _PhoneNumber = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("PhoneNumber");
                OnPhoneNumberChanged();
            }
        }
        private global::System.String _PhoneNumber;
        partial void OnPhoneNumberChanging(global::System.String value);
        partial void OnPhoneNumberChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String NumberType
        {
            get
            {
                return _NumberType;
            }
            set
            {
                OnNumberTypeChanging(value);
                ReportPropertyChanging("NumberType");
                _NumberType = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("NumberType");
                OnNumberTypeChanged();
            }
        }
        private global::System.String _NumberType;
        partial void OnNumberTypeChanging(global::System.String value);
        partial void OnNumberTypeChanged();

        #endregion
    
    }

    #endregion
    
}
