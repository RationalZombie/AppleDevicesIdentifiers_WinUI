using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AppleDevicesIdentifiers_WinUI
{
    public class DeviceItem
    {
        public string Name { get; set; }
        public string Identifier { get; set; }
        public DeviceItem(string name, string identifier)
        {
            Name = name;
            Identifier = identifier;
        }
    }
    public sealed partial class MainWindow : Window
    {
        private readonly Dictionary<string, List<DeviceItem>> _deviceDatabase = new Dictionary<string, List<DeviceItem>>();
        // 引入原生的 GetDpiForWindow
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern uint GetDpiForWindow(IntPtr hwnd); public MainWindow()
        {
            InitializeComponent();
            // 【关键修改 1】：将应用程序内容延伸到标题栏区域
            ExtendsContentIntoTitleBar = true;
            // 【关键修改 2】：指定哪个控件充当窗口的标题栏（负责响应拖拽、双击最大化等行为）
            SetTitleBar(CustomTitleBar);

            // 【新增】在窗口绘制前直接计算缩放并调整大小，彻底告别闪烁
            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            uint dpi = GetDpiForWindow(hWnd);
            double scale = dpi / 96.0;

            int scaledWidth = (int)(500 * scale);
            int scaledHeight = (int)(500 * scale);
            AppWindow.Resize(new Windows.Graphics.SizeInt32(scaledWidth, scaledHeight));

            AppWindow.Changed += AppWindow_Changed;

            InitializeDeviceDatabase();
        }

        #region 1. Data Initialization
        private void InitializeDeviceDatabase()
        {
            // iPhone List
            _deviceDatabase["iPhone"] = new List<DeviceItem>
            {
                new DeviceItem("iPhone", "iPhone1,1 (Original)"),
                new DeviceItem("iPhone 3G", "iPhone1,2 (3G)"),
                new DeviceItem("iPhone 3GS", "iPhone2,1 (3GS)"),
                new DeviceItem("iPhone 4", "iPhone3,1 (GSM), iPhone3,2 (GSM Rev A), iPhone3,3 (CDMA)"),
                new DeviceItem("iPhone 4S", "iPhone4,1 (Dual-Band)"),
                new DeviceItem("iPhone 5", "iPhone5,1 (GSM North America), iPhone5,2 (Global/CDMA)"),
                new DeviceItem("iPhone 5C", "iPhone5,3 (GSM North America), iPhone5,4 (Global/China)"),
                new DeviceItem("iPhone 5S", "iPhone6,1 (GSM North America), iPhone6,2 (Global/China)"),
                new DeviceItem("iPhone 6", "iPhone7,2 "),
                new DeviceItem("iPhone 6 Plus", "iPhone7,1"),
                new DeviceItem("iPhone 6S", "iPhone8,1"),
                new DeviceItem("iPhone 6S Plus", "iPhone8,2"),
                new DeviceItem("iPhone SE", "iPhone8,4 "),
                new DeviceItem("iPhone 7", "iPhone9,1 (Qualcomm/CDMA), iPhone9,3 (Intel/GSM)"),
                new DeviceItem("iPhone 7 Plus", "iPhone9,2 (Qualcomm/CDMA), iPhone9,4 (Intel/GSM)"),
                new DeviceItem("iPhone 8", "iPhone10,1 (Qualcomm/CDMA), iPhone10,4 (Intel/GSM)"),
                new DeviceItem("iPhone 8 Plus", "iPhone10,2 (Qualcomm/CDMA), iPhone10,5 (Intel/GSM)"),
                new DeviceItem("iPhone X", "iPhone10,3 (Qualcomm/CDMA), iPhone10,6 (Intel/GSM)"),
                new DeviceItem("iPhone XS", "iPhone11,2"),
                new DeviceItem("iPhone XS Max", "iPhone11,4 , iPhone11,6 (China Dual-SIM)"),
                new DeviceItem("iPhone XR", "iPhone11,8"),
                new DeviceItem("iPhone 11", "iPhone12,1"),
                new DeviceItem("iPhone 11 Pro", "iPhone12,3"),
                new DeviceItem("iPhone 11 Pro Max", "iPhone12,5"),
                new DeviceItem("iPhone SE 2", "iPhone12,8 (2nd Gen)"),
                new DeviceItem("iPhone 12 mini", "iPhone13,1"),
                new DeviceItem("iPhone 12", "iPhone13,2"),
                new DeviceItem("iPhone 12 Pro", "iPhone13,3"),
                new DeviceItem("iPhone 12 Pro Max", "iPhone13,4"),
                new DeviceItem("iPhone 13 mini", "iPhone14,4"),
                new DeviceItem("iPhone 13", "iPhone14,5"),
                new DeviceItem("iPhone 13 Pro", "iPhone14,2"),
                new DeviceItem("iPhone 13 Pro Max", "iPhone14,3"),
                new DeviceItem("iPhone SE 3", "iPhone14,6 (3rd Gen)"),
                new DeviceItem("iPhone 14", "iPhone14,7"),
                new DeviceItem("iPhone 14 Plus", "iPhone14,8"),
                new DeviceItem("iPhone 14 Pro", "iPhone15,2"),
                new DeviceItem("iPhone 14 Pro Max", "iPhone15,3"),
                new DeviceItem("iPhone 15", "iPhone15,4"),
                new DeviceItem("iPhone 15 Plus", "iPhone15,5"),
                new DeviceItem("iPhone 15 Pro", "iPhone16,1"),
                new DeviceItem("iPhone 15 Pro Max", "iPhone16,2"),
                new DeviceItem("iPhone 16", "iPhone17,3"),
                new DeviceItem("iPhone 16 Plus", "iPhone17,4"),
                new DeviceItem("iPhone 16 Pro", "iPhone17,1"),
                new DeviceItem("iPhone 16 Pro Max", "iPhone17,2"),
                new DeviceItem("iPhone 16e", "iPhone17,5 (SE Series)"),
                new DeviceItem("iPhone 17 Pro", "iPhone18,1"),
                new DeviceItem("iPhone 17 Pro Max", "iPhone18,2"),
                new DeviceItem("iPhone 17", "iPhone18,3"),
                new DeviceItem("iPhone Air", "iPhone18,4"),
                new DeviceItem("iPhone 17e", "iPhone18,5")
            };

            // iPad List
            _deviceDatabase["iPad"] = new List<DeviceItem>
            {
                new DeviceItem("iPad", "iPad1,1 (Wi-Fi/Cellular)"),
                new DeviceItem("iPad 2", "iPad2,1 (Wi-Fi), iPad2,2 (GSM), iPad2,3 (CDMA), iPad2,4 (Wi-Fi Rev A)"),
                new DeviceItem("iPad 3", "iPad3,1 (Wi-Fi), iPad3,2 (CDMA), iPad3,3 (GSM)"),
                new DeviceItem("iPad 4", "iPad3,4 (Wi-Fi), iPad3,5 (GSM), iPad3,6 (MM/CDMA)"),
                new DeviceItem("iPad 5", "iPad6,11 (Wi-Fi), iPad6,12 (Cellular)"),
                new DeviceItem("iPad 6", "iPad7,5 (Wi-Fi), iPad7,6 (Cellular)"),
                new DeviceItem("iPad 7", "iPad7,11 (Wi-Fi), iPad7,12 (Cellular)"),
                new DeviceItem("iPad 8", "iPad11,6 (Wi-Fi), iPad11,7 (Cellular)"),
                new DeviceItem("iPad 9", "iPad12,1 (Wi-Fi), iPad12,2 (Cellular)"),
                new DeviceItem("iPad 10", "iPad13,18 (Wi-Fi), iPad13,19 (Cellular)"),
                new DeviceItem("iPad (A16)", "iPad15,7 (Wi-Fi), iPad15,8 (Cellular)"),
                new DeviceItem("iPad Air", "iPad4,1 (Wi-Fi), iPad4,2 (Cellular), iPad4,3 (China)"),
                new DeviceItem("iPad Air 2", "iPad5,3 (Wi-Fi), iPad5,4 (Cellular)"),
                new DeviceItem("iPad Air 3", "iPad11,3 (Wi-Fi), iPad11,4 (Cellular)"),
                new DeviceItem("iPad Air 4", "iPad13,1 (Wi-Fi), iPad13,2 (Cellular)"),
                new DeviceItem("iPad Air 5", "iPad13,16 (Wi-Fi), iPad13,17 (Cellular)"),
                new DeviceItem("iPad Air 11-inch (M2)", "iPad14,8 (Wi-Fi), iPad14,9 (Cellular)"),
                new DeviceItem("iPad Air 11-inch (M3)", "iPad15,3 (Wi-Fi), iPad15,4 (Cellular)"),
                new DeviceItem("iPad Air 11-inch (M4)", "iPad16,8 (Wi-Fi), iPad16,9 (Cellular)"),
                new DeviceItem("iPad Air 13-inch (M2)", "iPad14,10 (Wi-Fi), iPad14,11 (Cellular)"),
                new DeviceItem("iPad Air 13-inch (M3)", "iPad15,5 (Wi-Fi), iPad15,6 (Cellular)"),
                new DeviceItem("iPad Air 13-inch (M4)", "iPad16,10 (Wi-Fi), iPad16,11 (Cellular)"),
                new DeviceItem("iPad Mini", "iPad2,5 (Wi-Fi), iPad2,6 (GSM), iPad2,7 (CDMA)"),
                new DeviceItem("iPad Mini 2", "iPad4,4 (Wi-Fi), iPad4,5 (Cellular), iPad4,6 (China)"),
                new DeviceItem("iPad Mini 3", "iPad4,7 (Wi-Fi), iPad4,8 (Cellular), iPad4,9 (China)"),
                new DeviceItem("iPad Mini 4", "iPad5,1 (Wi-Fi), iPad5,2 (Cellular)"),
                new DeviceItem("iPad Mini 5", "iPad11,1 (Wi-Fi), iPad11,2 (Cellular)"),
                new DeviceItem("iPad Mini 6", "iPad14,1 (Wi-Fi), iPad14,2 (Cellular)"),
                new DeviceItem("iPad Mini (A17 Pro)", "iPad16,1 (Wi-Fi), iPad16,2 (Cellular)"),
                new DeviceItem("iPad Pro 9.7-inch", "iPad6,3 (Wi-Fi), iPad6,4 (Cellular)"),
                new DeviceItem("iPad Pro 10.5-inch", "iPad7,3 (Wi-Fi), iPad7,4 (Cellular)"),
                new DeviceItem("iPad Pro 11-inch", "iPad8,1 (Wi-Fi), iPad8,2 (Wi-Fi 1TB), iPad8,3 (Cellular), iPad8,4 (Cellular 1TB)"),
                new DeviceItem("iPad Pro 11-inch 2", "iPad8,9 (Wi-Fi), iPad8,10 (Cellular)"),
                new DeviceItem("iPad Pro 11-inch 3", "iPad13,4 (Wi-Fi), iPad13,5 (Wi-Fi Global), iPad13,6 (Cellular), iPad13,7 (Cellular China)"),
                new DeviceItem("iPad Pro 11-inch (M2)", "iPad14,3 (Wi-Fi), iPad14,4 (Cellular)"),
                new DeviceItem("iPad Pro 11-inch (M4)", "iPad16,3 (Wi-Fi), iPad16,4 (Cellular)"),
                new DeviceItem("iPad Pro 11-inch (M5)", "iPad17,1 (Wi-Fi), iPad17,2 (Cellular)"),
                new DeviceItem("iPad Pro 12.9-inch", "iPad6,7 (Wi-Fi), iPad6,8 (Cellular)"),
                new DeviceItem("iPad Pro 12.9-inch 2", "iPad7,1 (Wi-Fi), iPad7,2 (Cellular)"),
                new DeviceItem("iPad Pro 12.9-inch 3", "iPad8,5 (Wi-Fi), iPad8,6 (Wi-Fi 1TB), iPad8,7 (Cellular), iPad8,8 (Cellular 1TB)"),
                new DeviceItem("iPad Pro 12.9-inch 4", "iPad8,11 (Wi-Fi), iPad8,12 (Cellular)"),
                new DeviceItem("iPad Pro 12.9-inch 5", "iPad13,8 (Wi-Fi), iPad13,9 (Wi-Fi Global), iPad13,10 (Cellular), iPad13,11 (Cellular China)"),
                new DeviceItem("iPad Pro 12.9-inch (M2)", "iPad14,5 (Wi-Fi), iPad14,6 (Cellular)"),
                new DeviceItem("iPad Pro 13-inch (M4)", "iPad16,5 (Wi-Fi), iPad16,6 (Cellular)"),
                new DeviceItem("iPad Pro 13-inch (M5)", "iPad17,3 (Wi-Fi), iPad17,4 (Cellular)")
            };

            // iPod List
            _deviceDatabase["iPod"] = new List<DeviceItem>
            {
                new DeviceItem("iPod Touch", "iPod1,1"),
                new DeviceItem("iPod Touch 2", "iPod2,1"),
                new DeviceItem("iPod Touch 3", "iPod3,1"),
                new DeviceItem("iPod Touch 4", "iPod4,1"),
                new DeviceItem("iPod Touch 5", "iPod5,1"),
                new DeviceItem("iPod Touch 6", "iPod7,1"),
                new DeviceItem("iPod Touch 7", "iPod9,1")
            };

            // Apple TV List
            _deviceDatabase["Apple TV"] = new List<DeviceItem>
            {
                new DeviceItem("Apple TV 1", "AppleTV1,1 (1st Gen)"),
                new DeviceItem("Apple TV 2", "AppleTV2,1 (2nd Gen)"),
                new DeviceItem("Apple TV 3", "AppleTV3,1 (3rd Gen), AppleTV3,2 (3rd Gen Rev A)"),
                new DeviceItem("Apple TV 4", "AppleTV5,3 (HD)"),
                new DeviceItem("Apple TV 4K", "AppleTV6,2 (1st Gen)"),
                new DeviceItem("Apple TV 4K 2", "AppleTV11,1 (2nd Gen)"),
                new DeviceItem("Apple TV 4K 3", "AppleTV14,1 (3rd Gen)")
            };

            // Apple Watch List
            _deviceDatabase["Apple Watch"] = new List<DeviceItem>
            {
                new DeviceItem("Apple Watch", "Watch1,1 (38mm), Watch1,2 (42mm)"),
                new DeviceItem("Apple Watch Series 1", "Watch2,6 (38mm), Watch2,7 (42mm)"),
                new DeviceItem("Apple Watch Series 2", "Watch2,3 (38mm), Watch2,4 (42mm)"),
                new DeviceItem("Apple Watch Series 3", "Watch3,1 (38mm Cellular), Watch3,2 (42mm Cellular), Watch3,3 (38mm Wi-Fi), Watch3,4 (42mm Wi-Fi)"),
                new DeviceItem("Apple Watch Series 4", "Watch4,1 (40mm Wi-Fi), Watch4,2 (44mm Wi-Fi), Watch4,3 (40mm Cellular), Watch4,4 (44mm Cellular)"),
                new DeviceItem("Apple Watch Series 5", "Watch5,1 (40mm Wi-Fi), Watch5,2 (44mm Wi-Fi), Watch5,3 (40mm Cellular), Watch5,4 (44mm Cellular)"),
                new DeviceItem("Apple Watch SE", "Watch5,9 (40mm Wi-Fi), Watch5,10 (44mm Wi-Fi), Watch5,11 (40mm Cellular), Watch5,12 (44mm Cellular)"),
                new DeviceItem("Apple Watch Series 6", "Watch6,1 (40mm Wi-Fi), Watch6,2 (44mm Wi-Fi), Watch6,3 (40mm Cellular), Watch6,4 (44mm Cellular)"),
                new DeviceItem("Apple Watch Series 7", "Watch6,6 (41mm Wi-Fi), Watch6,7 (45mm Wi-Fi), Watch6,8 (41mm Cellular), Watch6,9 (45mm Cellular)"),
                new DeviceItem("Apple Watch SE 2", "Watch6,10 (40mm Wi-Fi), Watch6,11 (44mm Wi-Fi), Watch6,12 (40mm Cellular), Watch6,13 (44mm Cellular)"),
                new DeviceItem("Apple Watch Series 8", "Watch6,14 (41mm Wi-Fi), Watch6,15 (45mm Wi-Fi), Watch6,16 (41mm Cellular), Watch6,17 (45mm Cellular)"),
                new DeviceItem("Apple Watch Ultra", "Watch6,18 (49mm Cellular)"),
                new DeviceItem("Apple Watch Series 9", "Watch7,1 (41mm Wi-Fi), Watch7,2 (45mm Wi-Fi), Watch7,3 (41mm Cellular), Watch7,4 (45mm Cellular)"),
                new DeviceItem("Apple Watch Ultra 2", "Watch7,5 (49mm Cellular)"),
                new DeviceItem("Apple Watch Series 10", "Watch7,8 (42mm Wi-Fi), Watch7,9 (46mm Wi-Fi), Watch7,10 (42mm Cellular), Watch7,11 (46mm Cellular)"),
                new DeviceItem("Apple Watch Ultra 3", "Watch7,12 (49mm Cellular)"),
                new DeviceItem("Apple Watch Series 11", "Watch7,17 (42mm Wi-Fi), Watch7,18 (46mm Wi-Fi), Watch7,19 (42mm Cellular), Watch7,20 (46mm Cellular)"),
                new DeviceItem("Apple Watch SE 3", "Watch7,13 (41mm Wi-Fi), Watch7,14 (45mm Wi-Fi), Watch7,15 (41mm Cellular), Watch7,16 (45mm Cellular)")
            };

            // HomePod List
            _deviceDatabase["HomePod"] = new List<DeviceItem>
            {
                new DeviceItem("HomePod", "AudioAccessory1,1 (1st Gen), AudioAccessory1,2 (2nd Gen)"),
                new DeviceItem("HomePod mini", "AudioAccessory5,1")
            };

            _deviceDatabase["Mac"] = new List<DeviceItem>();
        }
        #endregion

        #region 2. UI Interaction
        private void CmbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbType.SelectedItem is ComboBoxItem selectedTypeItem)
            {
                string categoryKey = selectedTypeItem.Tag?.ToString() ?? string.Empty;

                CmbModel.ItemsSource = null;
                LblIdentifier.Text = string.Empty;

                if (!string.IsNullOrEmpty(categoryKey) && _deviceDatabase.ContainsKey(categoryKey))
                {
                    CmbModel.ItemsSource = _deviceDatabase[categoryKey];
                    CmbModel.DisplayMemberPath = "Name";
                }
            }
        }

        private void CmbModel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbModel.SelectedItem is DeviceItem selectedDevice)
            {
                LblIdentifier.Text = selectedDevice.Identifier;
            }
            else
            {
                LblIdentifier.Text = string.Empty;
            }
        }
        #endregion

        #region 3.Windowing
        private void AppWindow_Changed(Microsoft.UI.Windowing.AppWindow sender, Microsoft.UI.Windowing.AppWindowChangedEventArgs args)
        {
            if (args.DidSizeChange)
            {
                UpdateTitleBarLayout();
            }
        }

        private void UpdateTitleBarLayout()
        {
            if (CustomTitleBar?.XamlRoot == null || AppWindow == null) return;

            double scale = CustomTitleBar.XamlRoot.RasterizationScale;
            double rightInsetDip = AppWindow.TitleBar.RightInset / scale;
            double leftInsetDip = AppWindow.TitleBar.LeftInset / scale;

            CustomTitleBar.Margin = new Thickness(leftInsetDip, 0, rightInsetDip, 0);
        }
        #endregion

    }
}