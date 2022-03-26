﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace MathsLibrary
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IService1" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IMathsOperations
    {
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Add?x={value1}&y={value2}", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        int Add(int value1, int value2);
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Mul?x={value1}&y={value2}", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]

        int Multiply(int value1, int value2);
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Sub?x={value1}&y={value2}", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]

        int Substract(int value1, int value2);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: ajoutez vos opérations de service ici
    }

    // Utilisez un contrat de données comme indiqué dans l'exemple ci-après pour ajouter les types composites aux opérations de service.
    // Vous pouvez ajouter des fichiers XSD au projet. Une fois le projet généré, vous pouvez utiliser directement les types de données qui y sont définis, avec l'espace de noms "MathsLibrary.ContractType".
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
