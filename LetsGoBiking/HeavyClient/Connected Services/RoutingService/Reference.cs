﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HeavyClient.RoutingService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GeoCoordinate", Namespace="http://schemas.datacontract.org/2004/07/System.Device.Location")]
    [System.SerializableAttribute()]
    public partial class GeoCoordinate : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double AltitudeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double CourseField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double HorizontalAccuracyField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double LatitudeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double LongitudeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double SpeedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double VerticalAccuracyField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double Altitude {
            get {
                return this.AltitudeField;
            }
            set {
                if ((this.AltitudeField.Equals(value) != true)) {
                    this.AltitudeField = value;
                    this.RaisePropertyChanged("Altitude");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double Course {
            get {
                return this.CourseField;
            }
            set {
                if ((this.CourseField.Equals(value) != true)) {
                    this.CourseField = value;
                    this.RaisePropertyChanged("Course");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double HorizontalAccuracy {
            get {
                return this.HorizontalAccuracyField;
            }
            set {
                if ((this.HorizontalAccuracyField.Equals(value) != true)) {
                    this.HorizontalAccuracyField = value;
                    this.RaisePropertyChanged("HorizontalAccuracy");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double Latitude {
            get {
                return this.LatitudeField;
            }
            set {
                if ((this.LatitudeField.Equals(value) != true)) {
                    this.LatitudeField = value;
                    this.RaisePropertyChanged("Latitude");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double Longitude {
            get {
                return this.LongitudeField;
            }
            set {
                if ((this.LongitudeField.Equals(value) != true)) {
                    this.LongitudeField = value;
                    this.RaisePropertyChanged("Longitude");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double Speed {
            get {
                return this.SpeedField;
            }
            set {
                if ((this.SpeedField.Equals(value) != true)) {
                    this.SpeedField = value;
                    this.RaisePropertyChanged("Speed");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double VerticalAccuracy {
            get {
                return this.VerticalAccuracyField;
            }
            set {
                if ((this.VerticalAccuracyField.Equals(value) != true)) {
                    this.VerticalAccuracyField = value;
                    this.RaisePropertyChanged("VerticalAccuracy");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CompositeType", Namespace="http://schemas.datacontract.org/2004/07/RoutingWithBikes")]
    [System.SerializableAttribute()]
    public partial class CompositeType : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool BoolValueField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StringValueField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool BoolValue {
            get {
                return this.BoolValueField;
            }
            set {
                if ((this.BoolValueField.Equals(value) != true)) {
                    this.BoolValueField = value;
                    this.RaisePropertyChanged("BoolValue");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string StringValue {
            get {
                return this.StringValueField;
            }
            set {
                if ((object.ReferenceEquals(this.StringValueField, value) != true)) {
                    this.StringValueField = value;
                    this.RaisePropertyChanged("StringValue");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="RoutingService.IService1")]
    public interface IService1 {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetData", ReplyAction="http://tempuri.org/IService1/GetDataResponse")]
        string GetData(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetData", ReplyAction="http://tempuri.org/IService1/GetDataResponse")]
        System.Threading.Tasks.Task<string> GetDataAsync(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/Test", ReplyAction="http://tempuri.org/IService1/TestResponse")]
        string Test();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/Test", ReplyAction="http://tempuri.org/IService1/TestResponse")]
        System.Threading.Tasks.Task<string> TestAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetTwoClosestStations", ReplyAction="http://tempuri.org/IService1/GetTwoClosestStationsResponse")]
        System.Tuple<HeavyClient.RoutingService.GeoCoordinate, HeavyClient.RoutingService.GeoCoordinate> GetTwoClosestStations(System.Tuple<System.Tuple<double, double>, System.Tuple<double, double>> locations);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetTwoClosestStations", ReplyAction="http://tempuri.org/IService1/GetTwoClosestStationsResponse")]
        System.Threading.Tasks.Task<System.Tuple<HeavyClient.RoutingService.GeoCoordinate, HeavyClient.RoutingService.GeoCoordinate>> GetTwoClosestStationsAsync(System.Tuple<System.Tuple<double, double>, System.Tuple<double, double>> locations);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetDataUsingDataContract", ReplyAction="http://tempuri.org/IService1/GetDataUsingDataContractResponse")]
        HeavyClient.RoutingService.CompositeType GetDataUsingDataContract(HeavyClient.RoutingService.CompositeType composite);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetDataUsingDataContract", ReplyAction="http://tempuri.org/IService1/GetDataUsingDataContractResponse")]
        System.Threading.Tasks.Task<HeavyClient.RoutingService.CompositeType> GetDataUsingDataContractAsync(HeavyClient.RoutingService.CompositeType composite);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/Add", ReplyAction="http://tempuri.org/IService1/AddResponse")]
        int Add(int value1, int value2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/Add", ReplyAction="http://tempuri.org/IService1/AddResponse")]
        System.Threading.Tasks.Task<int> AddAsync(int value1, int value2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/Multiply", ReplyAction="http://tempuri.org/IService1/MultiplyResponse")]
        int Multiply(int value1, int value2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/Multiply", ReplyAction="http://tempuri.org/IService1/MultiplyResponse")]
        System.Threading.Tasks.Task<int> MultiplyAsync(int value1, int value2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/Substract", ReplyAction="http://tempuri.org/IService1/SubstractResponse")]
        int Substract(int value1, int value2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/Substract", ReplyAction="http://tempuri.org/IService1/SubstractResponse")]
        System.Threading.Tasks.Task<int> SubstractAsync(int value1, int value2);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IService1Channel : HeavyClient.RoutingService.IService1, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service1Client : System.ServiceModel.ClientBase<HeavyClient.RoutingService.IService1>, HeavyClient.RoutingService.IService1 {
        
        public Service1Client() {
        }
        
        public Service1Client(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Service1Client(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetData(int value) {
            return base.Channel.GetData(value);
        }
        
        public System.Threading.Tasks.Task<string> GetDataAsync(int value) {
            return base.Channel.GetDataAsync(value);
        }
        
        public string Test() {
            return base.Channel.Test();
        }
        
        public System.Threading.Tasks.Task<string> TestAsync() {
            return base.Channel.TestAsync();
        }
        
        public System.Tuple<HeavyClient.RoutingService.GeoCoordinate, HeavyClient.RoutingService.GeoCoordinate> GetTwoClosestStations(System.Tuple<System.Tuple<double, double>, System.Tuple<double, double>> locations) {
            return base.Channel.GetTwoClosestStations(locations);
        }
        
        public System.Threading.Tasks.Task<System.Tuple<HeavyClient.RoutingService.GeoCoordinate, HeavyClient.RoutingService.GeoCoordinate>> GetTwoClosestStationsAsync(System.Tuple<System.Tuple<double, double>, System.Tuple<double, double>> locations) {
            return base.Channel.GetTwoClosestStationsAsync(locations);
        }
        
        public HeavyClient.RoutingService.CompositeType GetDataUsingDataContract(HeavyClient.RoutingService.CompositeType composite) {
            return base.Channel.GetDataUsingDataContract(composite);
        }
        
        public System.Threading.Tasks.Task<HeavyClient.RoutingService.CompositeType> GetDataUsingDataContractAsync(HeavyClient.RoutingService.CompositeType composite) {
            return base.Channel.GetDataUsingDataContractAsync(composite);
        }
        
        public int Add(int value1, int value2) {
            return base.Channel.Add(value1, value2);
        }
        
        public System.Threading.Tasks.Task<int> AddAsync(int value1, int value2) {
            return base.Channel.AddAsync(value1, value2);
        }
        
        public int Multiply(int value1, int value2) {
            return base.Channel.Multiply(value1, value2);
        }
        
        public System.Threading.Tasks.Task<int> MultiplyAsync(int value1, int value2) {
            return base.Channel.MultiplyAsync(value1, value2);
        }
        
        public int Substract(int value1, int value2) {
            return base.Channel.Substract(value1, value2);
        }
        
        public System.Threading.Tasks.Task<int> SubstractAsync(int value1, int value2) {
            return base.Channel.SubstractAsync(value1, value2);
        }
    }
}
