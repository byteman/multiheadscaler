using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Reflection;

namespace Monitor
{
    public enum Purview
    {
        None,
        Driver,
        Admin,
        CtrlAdmin
    }

    public class UserManage
    {
        private FormFrame formFrame = null;
        private Purview _CurPurview;
        private string _DriverPwd;
        private string _AdminPwd;
        //private string _CtrlAdmin;
        private string _FilePath;

        private const string cstrDriver = "driver";
        private const string cstrAdmin = "admin";

        public void Init(FormFrame f)
        {
            formFrame = f;
            _FilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName) + @"\pwd.xml";
            if(!File.Exists(_FilePath))
            {
                CreateConfigValue(_FilePath);
            }
            _CurPurview = Purview.None;
            _DriverPwd = GetConfigValue(_FilePath, cstrDriver);
            _AdminPwd = GetConfigValue(_FilePath, cstrAdmin);
            //_CtrlAdmin = "";
        }

        public Purview CurPurview
        {
            get { return _CurPurview; }
            set { _CurPurview = value;  }
        }

        public void UserInputPwd(Purview pv, string strPwd)
        {
            //_CurPurview = Purview.None;     //假设无权限
            //假设权限不变

            switch (pv)
            { 
                case Purview.None:
                    break;
                case Purview.Driver:
                    if (strPwd == _DriverPwd)
                    {
                        _CurPurview = Purview.Driver;
                    }
                    break;
                case Purview.Admin:
                    if (strPwd == _AdminPwd)
                    {
                        _CurPurview = Purview.Admin;
                    }
                    break;
                case Purview.CtrlAdmin:
                    if (strPwd == "------")
                    {
                        _CurPurview = Purview.CtrlAdmin;
                        break;
                    }

                    bool bSuccess;
                    int nCtrlPwd;
                    bSuccess = formFrame.ucCommon.GetCtrlPwd(out nCtrlPwd);
                    if (bSuccess)
                    {
                        if (strPwd == nCtrlPwd.ToString())
                        {
                            _CurPurview = Purview.CtrlAdmin;
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        public bool GetPwd(Purview pv, out string strPwd)
        {
            bool bSuccess;
            switch (pv)
            {
                case Purview.Driver:
                    strPwd = _DriverPwd;
                    bSuccess = true;
                    break;
                case Purview.Admin:
                    strPwd = _AdminPwd;
                    bSuccess = true;
                    break;
                case Purview.CtrlAdmin:
                    int nCtrlPwd;
                    bSuccess = formFrame.ucCommon.GetCtrlPwd(out nCtrlPwd);
                    strPwd = nCtrlPwd.ToString();
                    break;
                default:
                    strPwd = "";
                    bSuccess = false;
                    break;
            }
            return bSuccess;
        }

        public void ChangePwd(Purview pv, string pwd)
        {
            switch (pv)
            {
                case Purview.Driver:
                    EditConfigValue(_FilePath, cstrDriver, pwd);
                    _DriverPwd = pwd;
                    break;
                case Purview.Admin:
                    EditConfigValue(_FilePath, cstrAdmin, pwd);
                    _AdminPwd = pwd;
                    break;
                case Purview.CtrlAdmin:
                    formFrame.ucCommon.ChangeCtrlPwd(Convert.ToInt32(pwd));
                    break;
                default:
                    break;
            }
        }

        private string GetConfigValue(string path, string appKey)
        {
            XmlDocument xDoc = new XmlDocument();
            try
            {
                xDoc.Load(path);
                XmlNode xNode;
                XmlElement xElem;
                xNode = xDoc.SelectSingleNode("//appSettings");
                xElem = (XmlElement)xNode.SelectSingleNode("//add[@key='" + appKey + "']");
                if (xElem != null)
                    return xElem.GetAttribute("value");
                else
                    return "";
            }
            catch
            {
                return "";
            }
        }

        private void EditConfigValue(string path, string appKey, string strValue)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path);
            XmlNode xNode;
            XmlElement xElem;
            xNode = xDoc.SelectSingleNode("//appSettings");
            xElem = (XmlElement)xNode.SelectSingleNode("//add[@key='" + appKey + "']");
            if (xElem != null)
                xElem.Attributes[1].InnerText = strValue;
            xDoc.Save(path);
        }

        private void CreateConfigValue(string path)
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            doc.AppendChild(dec);
            XmlElement root = doc.CreateElement("appSettings");
            doc.AppendChild(root);

            XmlElement element = doc.CreateElement("add");
            element.SetAttribute("key", cstrDriver);
            element.SetAttribute("value", "0");
            root.AppendChild(element);

            element = doc.CreateElement("add");
            element.SetAttribute("key", cstrAdmin);
            element.SetAttribute("value", "0");
            root.AppendChild(element);

            doc.Save(path);
        }
    }
}
