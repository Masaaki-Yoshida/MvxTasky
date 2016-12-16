Webサイトまたはmarkdownプレビューとして表示している場合は、それぞれの枠内をコピー＆ペーストしてください。　　
メモ帳などのテキストエディタで開いている場合は、cs と の間の行をコピー＆ペーストしてください。　　

順次コードを張り付けていく部分もスニペットを用意していますが、その後の完成形も用意してありますので、適宜ご利用ください。　　

### TipView.xaml
```xaml
<Page
    x:Class="MvxTipCalc.UWP.Views.TipView"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:local="using:MvxTipCalc.UWP"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    mc:Ignorable="d">

    <Grid Background="{ThemeResource ApplicationPageBackgroundThemeBrush}">
        <StackPanel Margin="20,20,20,20" Orientation="Vertical">

            <TextBlock Text="SubTotal" />

            <TextBox
                x:Name="subTotal"
                Margin="20,0,20,0"
                InputScope="Number" />

            <TextBlock
                Margin="0,20,0,0"
                FontSize="24"
                Text="How Generous?" />
            <Slider
                x:Name="slider"
                Margin="20,0,20,0"
                LargeChange="10"
                Maximum="100"
                Minimum="0"
                SmallChange="1" />
            <TextBlock
                Margin="0,20,0,0"
                FontSize="24"
                Text="The Tip is:" />
            <TextBlock
                x:Name="tip"
                HorizontalAlignment="Center"
                FontSize="24" />
        </StackPanel>
    </Grid>
</Page>
```
### TipView.xaml.cs
```cs
public TipView()
{
    this.InitializeComponent();
    subTotal.TextChanged += SubTotal_TextChanged;
    slider.ValueChanged += Slider_ValueChanged;
}
```

```cs
private void SubTotal_TextChanged(object sender, TextChangedEventArgs e)
{
    double subTotalValue = 0;
    if (double.TryParse((sender as TextBox).Text, out subTotalValue)){
        tip.Text = TipAmount(subTotalValue, slider.Value).ToString();
    }
}

private void Slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
{
    int subTotalValue = 0;
    if (int.TryParse(subTotal.Text, out subTotalValue))
    {
        tip.Text = TipAmount(subTotalValue, slider.Value).ToString();
    }
}

public double TipAmount(double subTotal, double generosity)
{
    return subTotal * generosity / 100.0;
}
```

```cs
using MvxTipCalc.UWP.Views;
```

```cs
rootFrame.Navigate(typeof(MainPage), e.Arguments);    
↓
rootFrame.Navigate(typeof(TipView), e.Arguments);    
```

### TipView.xib
```xml
<?xml version="1.0" encoding="UTF-8" standalone="no"?>
<document type="com.apple.InterfaceBuilder3.CocoaTouch.XIB" version="3.0" toolsVersion="7706" systemVersion="14F27" targetRuntime="iOS.CocoaTouch" propertyAccessControl="none" useAutolayout="YES" useTraitCollections="YES">
    <dependencies>
        <plugIn identifier="com.apple.InterfaceBuilder.IBCocoaTouchPlugin" version="7703"/>
    </dependencies>
    <objects>
        <placeholder placeholderIdentifier="IBFilesOwner" id="-1" userLabel="File's Owner" customClass="TipView">
            <connections>
                <outlet property="view" destination="2" id="RRd-Eg-VrN"/>
                <outlet property="subTotal" destination="6" id="name-outlet-6"/>
                <outlet property="slider" destination="8" id="name-outlet-8"/>
                <outlet property="tip" destination="10" id="name-outlet-10"/>
            </connections>
        </placeholder>
        <placeholder placeholderIdentifier="IBFirstResponder" id="-2" customClass="UIResponder"/>
        <view contentMode="scaleToFill" id="2">
            <rect key="frame" x="0.0" y="0.0" width="600" height="600"/>
            <color key="backgroundColor" white="1" alpha="1" colorSpace="custom" customColorSpace="calibratedWhite"/>
            <subviews>
                <label opaque="NO" userInteractionEnabled="NO" contentMode="left" horizontalHuggingPriority="251" verticalHuggingPriority="251" text="SubTotal" lineBreakMode="tailTruncation" baselineAdjustment="alignBaselines" adjustsFontSizeToFit="NO" id="5" translatesAutoresizingMaskIntoConstraints="NO">
                    <rect key="frame" x="40" y="40" width="520" height="20"/>
                    <fontDescription key="fontDescription" type="system" pointSize="17"/>
                    <nil key="textColor"/>
                    <nil key="highlightedColor"/>
                </label>
                <textField opaque="NO" clipsSubviews="YES" contentMode="scaleToFill" contentHorizontalAlignment="left" contentVerticalAlignment="center" borderStyle="roundedRect" minimumFontSize="17" id="6" translatesAutoresizingMaskIntoConstraints="NO" textAlignment="right">
                    <rect key="frame" x="70" y="70" width="460" height="30"/>
                    <fontDescription key="fontDescription" type="system" pointSize="14"/>
                    <textInputTraits key="textInputTraits"/>
                </textField>
                <label opaque="NO" userInteractionEnabled="NO" contentMode="left" horizontalHuggingPriority="251" verticalHuggingPriority="251" text="How Generous?" textAlignment="natural" lineBreakMode="tailTruncation" baselineAdjustment="alignBaselines" adjustsFontSizeToFit="NO" id="7" translatesAutoresizingMaskIntoConstraints="NO">
                    <rect key="frame" x="40" y="130" width="520" height="21"/>
                    <fontDescription key="fontDescription" type="system" pointSize="17"/>
                    <nil key="textColor"/>
                    <nil key="highlightedColor"/>
                </label>
                <slider opaque="NO" contentMode="scaleToFill" contentHorizontalAlignment="center" contentVerticalAlignment="center" value="0.5" minValue="0.0" maxValue="1" id="8" translatesAutoresizingMaskIntoConstraints="NO">
                    <rect key="frame" x="68" y="171" width="464" height="31"/>
                </slider>
                <label opaque="NO" userInteractionEnabled="NO" contentMode="left" horizontalHuggingPriority="251" verticalHuggingPriority="251" text="The Tip is:" textAlignment="natural" lineBreakMode="tailTruncation" baselineAdjustment="alignBaselines" adjustsFontSizeToFit="NO" id="9" translatesAutoresizingMaskIntoConstraints="NO">
                    <rect key="frame" x="40" y="232" width="520" height="21"/>
                    <fontDescription key="fontDescription" type="system" pointSize="17"/>
                    <nil key="textColor"/>
                    <nil key="highlightedColor"/>
                    <constraints>
                        <constraint id="49" firstItem="9" firstAttribute="height" constant="21"/>
                    </constraints>
                </label>
                <label opaque="NO" userInteractionEnabled="NO" contentMode="left" horizontalHuggingPriority="251" verticalHuggingPriority="251" textAlignment="natural" lineBreakMode="tailTruncation" baselineAdjustment="alignBaselines" adjustsFontSizeToFit="NO" id="10" translatesAutoresizingMaskIntoConstraints="NO">
                    <rect key="frame" x="70" y="273" width="460" height="21"/>
                    <fontDescription key="fontDescription" type="system" pointSize="17"/>
                    <nil key="textColor"/>
                    <nil key="highlightedColor"/>
                    <constraints>
                        <constraint id="56" firstItem="10" firstAttribute="height" constant="21"/>
                    </constraints>
                </label>
            </subviews>
            <constraints>
                <constraint id="20" firstItem="5" firstAttribute="leading" secondItem="2" secondAttribute="leading" constant="40"/>
                <constraint id="21" firstItem="5" firstAttribute="top" secondItem="2" secondAttribute="top" constant="40"/>
                <constraint id="22" firstItem="2" firstAttribute="trailing" secondItem="5" secondAttribute="trailing" constant="40"/>
                <constraint id="25" firstItem="6" firstAttribute="leading" secondItem="2" secondAttribute="leading" constant="70"/>
                <constraint id="28" firstItem="6" firstAttribute="top" secondItem="5" secondAttribute="bottom" constant="10"/>
                <constraint id="29" firstItem="2" firstAttribute="trailing" secondItem="6" secondAttribute="trailing" constant="70"/>
                <constraint id="30" firstItem="7" firstAttribute="leading" secondItem="2" secondAttribute="leading" constant="40"/>
                <constraint id="31" firstItem="2" firstAttribute="trailing" secondItem="7" secondAttribute="trailing" constant="40"/>
                <constraint id="33" firstItem="7" firstAttribute="top" secondItem="6" secondAttribute="bottom" constant="30"/>
                <constraint id="37" firstItem="8" firstAttribute="top" secondItem="7" secondAttribute="bottom" constant="20"/>
                <constraint id="40" firstItem="2" firstAttribute="trailing" secondItem="8" secondAttribute="trailing" constant="70"/>
                <constraint id="41" firstItem="8" firstAttribute="leading" secondItem="2" secondAttribute="leading" constant="70"/>
                <constraint id="43" firstItem="9" firstAttribute="leading" secondItem="2" secondAttribute="leading" constant="40"/>
                <constraint id="44" firstItem="2" firstAttribute="trailing" secondItem="9" secondAttribute="trailing" constant="40"/>
                <constraint id="48" firstItem="9" firstAttribute="top" secondItem="8" secondAttribute="bottom" constant="30"/>
                <constraint id="50" firstItem="10" firstAttribute="leading" secondItem="2" secondAttribute="leading" constant="70"/>
                <constraint id="51" firstItem="2" firstAttribute="trailing" secondItem="10" secondAttribute="trailing" constant="70"/>
                <constraint id="57" firstItem="10" firstAttribute="top" secondItem="9" secondAttribute="bottom" constant="20"/>
            </constraints>
        </view>
    </objects>
</document>
```

### TipView.cs
```cs
public override void ViewDidLoad()
{
    base.ViewDidLoad();
    subTotal.EditingChanged += SubTotal_EditingChanged;
    slider.ValueChanged += Slider_ValueChanged;

    //EnterでKeyboardを閉じる制御
    subTotal.ShouldReturn += (textField) =>
    {
        subTotal.ResignFirstResponder();
        return true;
    };
    //欄外タップでKeyboardを閉じる制御
    var subTotalGesture = new UITapGestureRecognizer(() =>
    {
        this.subTotal.ResignFirstResponder();
    });
    View.AddGestureRecognizer(subTotalGesture);
}
```

```cs
private void SubTotal_EditingChanged(object sender, EventArgs e)
{
    double subTotalValue = 0;
    if (double.TryParse((sender as UITextField).Text, out subTotalValue))
    {
        tip.Text = TipAmount(subTotalValue, slider.Value).ToString();
    }
}

private void Slider_ValueChanged(object sender, EventArgs e)
{
    int subTotalValue = 0;
    if (int.TryParse(subTotal.Text, out subTotalValue))
    {
        tip.Text = TipAmount(subTotalValue, slider.Value).ToString();
    }
}

public double TipAmount(double subTotal, double generosity)
{
    return subTotal * generosity / 100.0;
}
```

```cs
using MvxTipCalc.iOS.Views;
```

```cs
public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
{
    Window = new UIWindow(UIScreen.MainScreen.Bounds);
    Window.RootViewController = new TipView();
    Window.MakeKeyAndVisible();
    return true;
}
```

### ICalculation.cs
```cs
namespace MvxTipCalc.Core.Services
{
    public interface ICalculation
    {
        double TipAmount(double subTotal, double  generosity);
    }
}
```

### Calculation.cs
```cs
namespace MvxTipCalc.Core.Services
{
    public class Calculation : ICalculation
    {
        public double TipAmount(double subTotal, double  generosity)
        {
            return subTotal * generosity / 100.0;
        }
    }
}
```

### TipViewModel.cs

```cs
using MvvmCross.Core.ViewModels;
using MvxTipCalc.Core.Services;

namespace MvxTipCalc.Core.ViewModels
{
    public class TipViewModel : MvxViewModel
    {
        readonly ICalculation _calculation;

        public TipViewModel(ICalculation calculation)
        {
            _calculation = calculation;
        }

        public override void Start()
        {
            _subTotal = 100;
            _generosity = 10;
            Recalcuate();
            base.Start();
        }

        double _subTotal;
        public double SubTotal
        {
            get { return _subTotal; }
            set
            {
                _subTotal = value;
                RaisePropertyChanged(() => SubTotal);
                Recalcuate();
            }
        }

        double _generosity;
        public double Generosity
        {
            get { return _generosity; }
            set
            {
                _generosity = value;
                RaisePropertyChanged(() => Generosity);
                Recalcuate();
            }
        }

        double _tip;
        public double Tip
        {
            get { return _tip; }
            set
            {
                _tip = value;
                RaisePropertyChanged(() => Tip);
            }
        }

        private void Recalcuate()
        {
            Tip = _calculation.TipAmount(SubTotal, Generosity);
        }
    }
}
```

### App.cs
```cs
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvxTipCalc.Core.Services;
using MvxTipCalc.Core.ViewModels;

namespace MvxTipCalc.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            Mvx.RegisterType<ICalculation, Calculation>();
            Mvx.RegisterSingleton<IMvxAppStart>(new MvxAppStart<TipViewModel>());
        }
    }
}
```

### Setup.cs
```cs
using MvvmCross.Core.ViewModels;
using MvvmCross.WindowsUWP.Platform;
using Windows.UI.Xaml.Controls;

namespace MvxTipCalc.UWP
{
    public class Setup : MvxWindowsSetup
    {
        public Setup(Frame rootFrame) : base(rootFrame)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new Core.App();
        }
    }
}
```

### App.xaml.cs
```cs
rootFrame.Navigate(typeof(TipView), e.Arguments);
↓
var setup = new Setup(rootFrame);
setup.Initialize();
var start = Mvx.Resolve<IMvxAppStart>();
start.Start();
```

### TipView.xaml.cs

```cs
using MvvmCross.WindowsUWP.Views;

public sealed partial class TipView : Page
↓
public sealed partial class TipView : MvxWindowsPage
```

```cs
using MvvmCross.WindowsUWP.Views;

namespace MvxTipCalc.UWP.Views
{
    public sealed partial class TipView : MvxWindowsPage
    {
        public TipView()
        {
            this.InitializeComponent();
        }
    }
}
```

### TipView.xaml

```xml
<views:MvxWindowsPage
    x:Class="MvxTipCalc.UWP.Views.TipView"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:local="using:MvxTipCalc.UWP"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    xmlns:views="using:MvvmCross.WindowsUWP.Views"
    mc:Ignorable="d">

    <Grid Background="{ThemeResource ApplicationPageBackgroundThemeBrush}">
        <StackPanel Margin="20,20,20,20" Orientation="Vertical">

            <TextBlock Text="SubTotal" />

            <TextBox
                Margin="20,0,20,0"
                InputScope="Number"
                Text="{Binding SubTotal, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}" />

            <TextBlock
                Margin="0,20,0,0"
                FontSize="24"
                Text="How Generous?" />
            <Slider
                Margin="20,0,20,0"
                LargeChange="10"
                Maximum="100"
                Minimum="0"
                SmallChange="1"
                Value="{Binding Generosity, Mode=TwoWay}" />
            <TextBlock
                Margin="0,20,0,0"
                FontSize="24"
                Text="The Tip is:" />
            <TextBlock
                HorizontalAlignment="Center"
                FontSize="24"
                Text="{Binding Tip}" />
        </StackPanel>
    </Grid>
</views:MvxWindowsPage>

```

#### Setup.cs
```cs
using MvvmCross.iOS.Platform;
using MvvmCross.iOS.Views.Presenters;
using MvvmCross.Core.ViewModels;
using MvxTipCalc.Core;

namespace MvxTipCalc.iOS
{
    public class Setup : MvxIosSetup
    {
        public Setup(MvxApplicationDelegate appDelegate, IMvxIosViewPresenter presenter)
            : base(appDelegate, presenter)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new App();
        }
    }
}
```

### AppDelegate.cs
```cs
using MvvmCross.iOS.Platform;
```

```cs
public class AppDelegate : ApplicationDelegate
↓
public class AppDelegate : MvxApplicationDelegate
```

```cs
Window.RootViewController = new TipView();
↓
var presenter = new MvxIosViewPresenter(this, Window);
var setup = new Setup(this, presenter);
setup.Initialize();
var startup = Mvx.Resolve<IMvxAppStart>();
startup.Start();
```

### TipView.cs
```cs
using MvvmCross.iOS.Views;

public partial class TipView : UIViewController
↓
public partial class TipView : MvxViewController
```

```cs
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using MvxTipCalc.Core.ViewModels;
using UIKit;

namespace MvxTipCalc.iOS.Views
{
    public partial class TipView : MvxViewController
    {
        public TipView() : base("TipView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            subTotal.ShouldReturn += (textField) =>
            {
                subTotal.ResignFirstResponder();
                return true;
            };
            var subTotalGesture = new UITapGestureRecognizer(() =>
            {
                this.subTotal.ResignFirstResponder();
            });
            View.AddGestureRecognizer(subTotalGesture);
        }
    }
}
```

```cs
base.ViewDidLoad();

this.CreateBinding(tip).To((TipViewModel vm) => vm.Tip).Apply();
this.CreateBinding(subTotal).To((TipViewModel vm) => vm.SubTotal).Apply();
this.CreateBinding(slider).To((TipViewModel vm) => vm.Generosity).Apply();
```
