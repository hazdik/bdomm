﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1433
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;



[System.Data.Linq.Mapping.DatabaseAttribute(Name="OMM")]
public partial class OMMDataContext : System.Data.Linq.DataContext
{
	
	private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
	
  #region Extensibility Method Definitions
  partial void OnCreated();
  partial void InsertMessage(Message instance);
  partial void UpdateMessage(Message instance);
  partial void DeleteMessage(Message instance);
  partial void InsertMessage_Recipient(Message_Recipient instance);
  partial void UpdateMessage_Recipient(Message_Recipient instance);
  partial void DeleteMessage_Recipient(Message_Recipient instance);
  partial void InsertMessage_Statuse(Message_Statuse instance);
  partial void UpdateMessage_Statuse(Message_Statuse instance);
  partial void DeleteMessage_Statuse(Message_Statuse instance);
  #endregion
	
	public OMMDataContext() : 
			base(global::System.Configuration.ConfigurationManager.ConnectionStrings["OMMConnectionString"].ConnectionString, mappingSource)
	{
		OnCreated();
	}
	
	public OMMDataContext(string connection) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public OMMDataContext(System.Data.IDbConnection connection) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public OMMDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public OMMDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public System.Data.Linq.Table<Message> Messages
	{
		get
		{
			return this.GetTable<Message>();
		}
	}
	
	public System.Data.Linq.Table<Message_Recipient> Message_Recipients
	{
		get
		{
			return this.GetTable<Message_Recipient>();
		}
	}
	
	public System.Data.Linq.Table<Message_Statuse> Message_Statuses
	{
		get
		{
			return this.GetTable<Message_Statuse>();
		}
	}
}

[Table(Name="dbo.Messages")]
public partial class Message : INotifyPropertyChanging, INotifyPropertyChanged
{
	
	private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
	
	private int _ID;
	
	private string _Text;
	
	private bool _Delivered;
	
	private EntitySet<Message_Recipient> _Message_Recipients;
	
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnTextChanging(string value);
    partial void OnTextChanged();
    partial void OnDeliveredChanging(bool value);
    partial void OnDeliveredChanged();
    #endregion
	
	public Message()
	{
		this._Message_Recipients = new EntitySet<Message_Recipient>(new Action<Message_Recipient>(this.attach_Message_Recipients), new Action<Message_Recipient>(this.detach_Message_Recipients));
		OnCreated();
	}
	
	[Column(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
	public int ID
	{
		get
		{
			return this._ID;
		}
		set
		{
			if ((this._ID != value))
			{
				this.OnIDChanging(value);
				this.SendPropertyChanging();
				this._ID = value;
				this.SendPropertyChanged("ID");
				this.OnIDChanged();
			}
		}
	}
	
	[Column(Storage="_Text", DbType="NVarChar(MAX)")]
	public string Text
	{
		get
		{
			return this._Text;
		}
		set
		{
			if ((this._Text != value))
			{
				this.OnTextChanging(value);
				this.SendPropertyChanging();
				this._Text = value;
				this.SendPropertyChanged("Text");
				this.OnTextChanged();
			}
		}
	}
	
	[Column(Storage="_Delivered", DbType="Bit NOT NULL")]
	public bool Delivered
	{
		get
		{
			return this._Delivered;
		}
		set
		{
			if ((this._Delivered != value))
			{
				this.OnDeliveredChanging(value);
				this.SendPropertyChanging();
				this._Delivered = value;
				this.SendPropertyChanged("Delivered");
				this.OnDeliveredChanged();
			}
		}
	}
	
	[Association(Name="Message_Message_Recipient", Storage="_Message_Recipients", OtherKey="Message_ID")]
	public EntitySet<Message_Recipient> Message_Recipients
	{
		get
		{
			return this._Message_Recipients;
		}
		set
		{
			this._Message_Recipients.Assign(value);
		}
	}
	
	public event PropertyChangingEventHandler PropertyChanging;
	
	public event PropertyChangedEventHandler PropertyChanged;
	
	protected virtual void SendPropertyChanging()
	{
		if ((this.PropertyChanging != null))
		{
			this.PropertyChanging(this, emptyChangingEventArgs);
		}
	}
	
	protected virtual void SendPropertyChanged(String propertyName)
	{
		if ((this.PropertyChanged != null))
		{
			this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
	
	private void attach_Message_Recipients(Message_Recipient entity)
	{
		this.SendPropertyChanging();
		entity.Message = this;
	}
	
	private void detach_Message_Recipients(Message_Recipient entity)
	{
		this.SendPropertyChanging();
		entity.Message = null;
	}
}

[Table(Name="dbo.Message_Recipients")]
public partial class Message_Recipient : INotifyPropertyChanging, INotifyPropertyChanged
{
	
	private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
	
	private int _ID;
	
	private int _Message_ID;
	
	private int _Recipient_ID;
	
	private string _Recipient_Name;
	
	private string _Destination;
	
	private int _Try_Order;
	
	private System.Nullable<decimal> _SMS_Credits;
	
	private bool _Is_Phone_Number;
	
	private System.Nullable<int> _SMS_ID;
	
	private System.Nullable<int> _SMS_Status_ID;
	
	private int _Status_ID;
	
	private System.DateTime _Updated_On;
	
	private EntityRef<Message> _Message;
	
	private EntityRef<Message_Statuse> _Message_Statuse;
	
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnMessage_IDChanging(int value);
    partial void OnMessage_IDChanged();
    partial void OnRecipient_IDChanging(int value);
    partial void OnRecipient_IDChanged();
    partial void OnRecipient_NameChanging(string value);
    partial void OnRecipient_NameChanged();
    partial void OnDestinationChanging(string value);
    partial void OnDestinationChanged();
    partial void OnTry_OrderChanging(int value);
    partial void OnTry_OrderChanged();
    partial void OnSMS_CreditsChanging(System.Nullable<decimal> value);
    partial void OnSMS_CreditsChanged();
    partial void OnIs_Phone_NumberChanging(bool value);
    partial void OnIs_Phone_NumberChanged();
    partial void OnSMS_IDChanging(System.Nullable<int> value);
    partial void OnSMS_IDChanged();
    partial void OnSMS_Status_IDChanging(System.Nullable<int> value);
    partial void OnSMS_Status_IDChanged();
    partial void OnStatus_IDChanging(int value);
    partial void OnStatus_IDChanged();
    partial void OnUpdated_OnChanging(System.DateTime value);
    partial void OnUpdated_OnChanged();
    #endregion
	
	public Message_Recipient()
	{
		this._Message = default(EntityRef<Message>);
		this._Message_Statuse = default(EntityRef<Message_Statuse>);
		OnCreated();
	}
	
	[Column(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
	public int ID
	{
		get
		{
			return this._ID;
		}
		set
		{
			if ((this._ID != value))
			{
				this.OnIDChanging(value);
				this.SendPropertyChanging();
				this._ID = value;
				this.SendPropertyChanged("ID");
				this.OnIDChanged();
			}
		}
	}
	
	[Column(Storage="_Message_ID", DbType="Int NOT NULL")]
	public int Message_ID
	{
		get
		{
			return this._Message_ID;
		}
		set
		{
			if ((this._Message_ID != value))
			{
				if (this._Message.HasLoadedOrAssignedValue)
				{
					throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
				}
				this.OnMessage_IDChanging(value);
				this.SendPropertyChanging();
				this._Message_ID = value;
				this.SendPropertyChanged("Message_ID");
				this.OnMessage_IDChanged();
			}
		}
	}
	
	[Column(Storage="_Recipient_ID", DbType="Int NOT NULL")]
	public int Recipient_ID
	{
		get
		{
			return this._Recipient_ID;
		}
		set
		{
			if ((this._Recipient_ID != value))
			{
				this.OnRecipient_IDChanging(value);
				this.SendPropertyChanging();
				this._Recipient_ID = value;
				this.SendPropertyChanged("Recipient_ID");
				this.OnRecipient_IDChanged();
			}
		}
	}
	
	[Column(Storage="_Recipient_Name", DbType="NVarChar(150) NOT NULL", CanBeNull=false)]
	public string Recipient_Name
	{
		get
		{
			return this._Recipient_Name;
		}
		set
		{
			if ((this._Recipient_Name != value))
			{
				this.OnRecipient_NameChanging(value);
				this.SendPropertyChanging();
				this._Recipient_Name = value;
				this.SendPropertyChanged("Recipient_Name");
				this.OnRecipient_NameChanged();
			}
		}
	}
	
	[Column(Storage="_Destination", DbType="NVarChar(300) NOT NULL", CanBeNull=false)]
	public string Destination
	{
		get
		{
			return this._Destination;
		}
		set
		{
			if ((this._Destination != value))
			{
				this.OnDestinationChanging(value);
				this.SendPropertyChanging();
				this._Destination = value;
				this.SendPropertyChanged("Destination");
				this.OnDestinationChanged();
			}
		}
	}
	
	[Column(Storage="_Try_Order", DbType="Int NOT NULL")]
	public int Try_Order
	{
		get
		{
			return this._Try_Order;
		}
		set
		{
			if ((this._Try_Order != value))
			{
				this.OnTry_OrderChanging(value);
				this.SendPropertyChanging();
				this._Try_Order = value;
				this.SendPropertyChanged("Try_Order");
				this.OnTry_OrderChanged();
			}
		}
	}
	
	[Column(Storage="_SMS_Credits", DbType="SmallMoney")]
	public System.Nullable<decimal> SMS_Credits
	{
		get
		{
			return this._SMS_Credits;
		}
		set
		{
			if ((this._SMS_Credits != value))
			{
				this.OnSMS_CreditsChanging(value);
				this.SendPropertyChanging();
				this._SMS_Credits = value;
				this.SendPropertyChanged("SMS_Credits");
				this.OnSMS_CreditsChanged();
			}
		}
	}
	
	[Column(Storage="_Is_Phone_Number", DbType="Bit NOT NULL")]
	public bool Is_Phone_Number
	{
		get
		{
			return this._Is_Phone_Number;
		}
		set
		{
			if ((this._Is_Phone_Number != value))
			{
				this.OnIs_Phone_NumberChanging(value);
				this.SendPropertyChanging();
				this._Is_Phone_Number = value;
				this.SendPropertyChanged("Is_Phone_Number");
				this.OnIs_Phone_NumberChanged();
			}
		}
	}
	
	[Column(Storage="_SMS_ID", DbType="Int")]
	public System.Nullable<int> SMS_ID
	{
		get
		{
			return this._SMS_ID;
		}
		set
		{
			if ((this._SMS_ID != value))
			{
				this.OnSMS_IDChanging(value);
				this.SendPropertyChanging();
				this._SMS_ID = value;
				this.SendPropertyChanged("SMS_ID");
				this.OnSMS_IDChanged();
			}
		}
	}
	
	[Column(Storage="_SMS_Status_ID", DbType="Int")]
	public System.Nullable<int> SMS_Status_ID
	{
		get
		{
			return this._SMS_Status_ID;
		}
		set
		{
			if ((this._SMS_Status_ID != value))
			{
				this.OnSMS_Status_IDChanging(value);
				this.SendPropertyChanging();
				this._SMS_Status_ID = value;
				this.SendPropertyChanged("SMS_Status_ID");
				this.OnSMS_Status_IDChanged();
			}
		}
	}
	
	[Column(Storage="_Status_ID", DbType="Int NOT NULL")]
	public int Status_ID
	{
		get
		{
			return this._Status_ID;
		}
		set
		{
			if ((this._Status_ID != value))
			{
				if (this._Message_Statuse.HasLoadedOrAssignedValue)
				{
					throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
				}
				this.OnStatus_IDChanging(value);
				this.SendPropertyChanging();
				this._Status_ID = value;
				this.SendPropertyChanged("Status_ID");
				this.OnStatus_IDChanged();
			}
		}
	}
	
	[Column(Storage="_Updated_On", DbType="DateTime NOT NULL")]
	public System.DateTime Updated_On
	{
		get
		{
			return this._Updated_On;
		}
		set
		{
			if ((this._Updated_On != value))
			{
				this.OnUpdated_OnChanging(value);
				this.SendPropertyChanging();
				this._Updated_On = value;
				this.SendPropertyChanged("Updated_On");
				this.OnUpdated_OnChanged();
			}
		}
	}
	
	[Association(Name="Message_Message_Recipient", Storage="_Message", ThisKey="Message_ID", IsForeignKey=true)]
	public Message Message
	{
		get
		{
			return this._Message.Entity;
		}
		set
		{
			Message previousValue = this._Message.Entity;
			if (((previousValue != value) 
						|| (this._Message.HasLoadedOrAssignedValue == false)))
			{
				this.SendPropertyChanging();
				if ((previousValue != null))
				{
					this._Message.Entity = null;
					previousValue.Message_Recipients.Remove(this);
				}
				this._Message.Entity = value;
				if ((value != null))
				{
					value.Message_Recipients.Add(this);
					this._Message_ID = value.ID;
				}
				else
				{
					this._Message_ID = default(int);
				}
				this.SendPropertyChanged("Message");
			}
		}
	}
	
	[Association(Name="Message_Statuse_Message_Recipient", Storage="_Message_Statuse", ThisKey="Status_ID", IsForeignKey=true)]
	public Message_Statuse Message_Statuse
	{
		get
		{
			return this._Message_Statuse.Entity;
		}
		set
		{
			Message_Statuse previousValue = this._Message_Statuse.Entity;
			if (((previousValue != value) 
						|| (this._Message_Statuse.HasLoadedOrAssignedValue == false)))
			{
				this.SendPropertyChanging();
				if ((previousValue != null))
				{
					this._Message_Statuse.Entity = null;
					previousValue.Message_Recipients.Remove(this);
				}
				this._Message_Statuse.Entity = value;
				if ((value != null))
				{
					value.Message_Recipients.Add(this);
					this._Status_ID = value.ID;
				}
				else
				{
					this._Status_ID = default(int);
				}
				this.SendPropertyChanged("Message_Statuse");
			}
		}
	}
	
	public event PropertyChangingEventHandler PropertyChanging;
	
	public event PropertyChangedEventHandler PropertyChanged;
	
	protected virtual void SendPropertyChanging()
	{
		if ((this.PropertyChanging != null))
		{
			this.PropertyChanging(this, emptyChangingEventArgs);
		}
	}
	
	protected virtual void SendPropertyChanged(String propertyName)
	{
		if ((this.PropertyChanged != null))
		{
			this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}

[Table(Name="dbo.Message_Statuses")]
public partial class Message_Statuse : INotifyPropertyChanging, INotifyPropertyChanged
{
	
	private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
	
	private int _ID;
	
	private string _Description;
	
	private EntitySet<Message_Recipient> _Message_Recipients;
	
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnDescriptionChanging(string value);
    partial void OnDescriptionChanged();
    #endregion
	
	public Message_Statuse()
	{
		this._Message_Recipients = new EntitySet<Message_Recipient>(new Action<Message_Recipient>(this.attach_Message_Recipients), new Action<Message_Recipient>(this.detach_Message_Recipients));
		OnCreated();
	}
	
	[Column(Storage="_ID", DbType="Int NOT NULL", IsPrimaryKey=true)]
	public int ID
	{
		get
		{
			return this._ID;
		}
		set
		{
			if ((this._ID != value))
			{
				this.OnIDChanging(value);
				this.SendPropertyChanging();
				this._ID = value;
				this.SendPropertyChanged("ID");
				this.OnIDChanged();
			}
		}
	}
	
	[Column(Storage="_Description", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
	public string Description
	{
		get
		{
			return this._Description;
		}
		set
		{
			if ((this._Description != value))
			{
				this.OnDescriptionChanging(value);
				this.SendPropertyChanging();
				this._Description = value;
				this.SendPropertyChanged("Description");
				this.OnDescriptionChanged();
			}
		}
	}
	
	[Association(Name="Message_Statuse_Message_Recipient", Storage="_Message_Recipients", OtherKey="Status_ID")]
	public EntitySet<Message_Recipient> Message_Recipients
	{
		get
		{
			return this._Message_Recipients;
		}
		set
		{
			this._Message_Recipients.Assign(value);
		}
	}
	
	public event PropertyChangingEventHandler PropertyChanging;
	
	public event PropertyChangedEventHandler PropertyChanged;
	
	protected virtual void SendPropertyChanging()
	{
		if ((this.PropertyChanging != null))
		{
			this.PropertyChanging(this, emptyChangingEventArgs);
		}
	}
	
	protected virtual void SendPropertyChanged(String propertyName)
	{
		if ((this.PropertyChanged != null))
		{
			this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
	
	private void attach_Message_Recipients(Message_Recipient entity)
	{
		this.SendPropertyChanging();
		entity.Message_Statuse = this;
	}
	
	private void detach_Message_Recipients(Message_Recipient entity)
	{
		this.SendPropertyChanging();
		entity.Message_Statuse = null;
	}
}
#pragma warning restore 1591
