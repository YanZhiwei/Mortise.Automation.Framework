using AutoMapper;
using FlaUI.Core.Definitions;
using Mortise.Accessibility.Abstractions;

namespace Mortise.UiaAccessibility;

internal class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<ControlType, AccessibleControlType>().ConvertUsing((value, _) =>
        {
            switch (value)
            {
                case ControlType.AppBar:
                    return AccessibleControlType.AppBar;
                case ControlType.Button:
                    return AccessibleControlType.Button;
                case ControlType.Calendar:
                    return AccessibleControlType.Calendar;
                case ControlType.CheckBox:
                    return AccessibleControlType.CheckBox;
                case ControlType.ComboBox:
                    return AccessibleControlType.ComboBox;
                case ControlType.Custom:
                    return AccessibleControlType.Custom;
                case ControlType.Edit:
                    return AccessibleControlType.Edit;
                case ControlType.DataGrid:
                    return AccessibleControlType.DataGrid;
                case ControlType.Document:
                    return AccessibleControlType.Document;
                case ControlType.Group:
                    return AccessibleControlType.Group;
                case ControlType.Header:
                    return AccessibleControlType.Header;
                case ControlType.Image:
                    return AccessibleControlType.Image;
                case ControlType.List:
                    return AccessibleControlType.List;
                case ControlType.ListItem:
                    return AccessibleControlType.ListItem;
                case ControlType.Menu:
                    return AccessibleControlType.Menu;
                case ControlType.MenuBar:
                    return AccessibleControlType.MenuBar;
                case ControlType.MenuItem:
                    return AccessibleControlType.MenuItem;
                case ControlType.Pane:
                    return AccessibleControlType.Pane;
                case ControlType.ProgressBar:
                    return AccessibleControlType.ProgressBar;
                case ControlType.RadioButton:
                    return AccessibleControlType.RadioButton;
                case ControlType.ScrollBar:
                    return AccessibleControlType.ScrollBar;
                case ControlType.Slider:
                    return AccessibleControlType.Slider;
                case ControlType.Spinner:
                    return AccessibleControlType.Spinner;
                case ControlType.StatusBar:
                    return AccessibleControlType.StatusBar;
                case ControlType.Tab:
                    return AccessibleControlType.Tab;
                case ControlType.TabItem:
                    return AccessibleControlType.TabItem;
                case ControlType.Table:
                    return AccessibleControlType.Table;
                case ControlType.Text:
                    return AccessibleControlType.Text;
                case ControlType.TitleBar:
                    return AccessibleControlType.TitleBar;
                case ControlType.ToolBar:
                    return AccessibleControlType.ToolBar;
                case ControlType.ToolTip:
                    return AccessibleControlType.ToolTip;
                case ControlType.Tree:
                    return AccessibleControlType.Tree;
                case ControlType.TreeItem:
                    return AccessibleControlType.TreeItem;
                case ControlType.Window:
                    return AccessibleControlType.Window;
                case ControlType.Separator:
                    return AccessibleControlType.Separator;
                case ControlType.SemanticZoom:
                    return AccessibleControlType.SemanticZoom;
                case ControlType.Thumb:
                    return AccessibleControlType.Thumb;
                case ControlType.HeaderItem:
                    return AccessibleControlType.HeaderItem;
                case ControlType.Hyperlink:
                    return AccessibleControlType.Hyperlink;
                case ControlType.SplitButton:
                    return AccessibleControlType.SplitButton;
                case ControlType.DataItem:
                    return AccessibleControlType.DataItem;
                default:
                    return AccessibleControlType.Unknown;
            }
        });

        CreateMap<AccessibleControlType, ControlType>().ConvertUsing((value, _) =>
        {
            switch (value)
            {
                case AccessibleControlType.AppBar:
                    return ControlType.AppBar;
                case AccessibleControlType.Button:
                    return ControlType.Button;
                case AccessibleControlType.Calendar:
                    return ControlType.Calendar;
                case AccessibleControlType.CheckBox:
                    return ControlType.CheckBox;
                case AccessibleControlType.ComboBox:
                    return ControlType.ComboBox;
                case AccessibleControlType.Custom:
                    return ControlType.Custom;
                case AccessibleControlType.Edit:
                    return ControlType.Edit;
                case AccessibleControlType.DataGrid:
                    return ControlType.DataGrid;
                case AccessibleControlType.Document:
                    return ControlType.Document;
                case AccessibleControlType.Group:
                    return ControlType.Group;
                case AccessibleControlType.Header:
                    return ControlType.Header;
                case AccessibleControlType.Image:
                    return ControlType.Image;
                case AccessibleControlType.List:
                    return ControlType.List;
                case AccessibleControlType.ListItem:
                    return ControlType.ListItem;
                case AccessibleControlType.Menu:
                    return ControlType.Menu;
                case AccessibleControlType.MenuBar:
                    return ControlType.MenuBar;
                case AccessibleControlType.MenuItem:
                    return ControlType.MenuItem;
                case AccessibleControlType.Pane:
                    return ControlType.Pane;
                case AccessibleControlType.ProgressBar:
                    return ControlType.ProgressBar;
                case AccessibleControlType.RadioButton:
                    return ControlType.RadioButton;
                case AccessibleControlType.ScrollBar:
                    return ControlType.ScrollBar;
                case AccessibleControlType.Slider:
                    return ControlType.Slider;
                case AccessibleControlType.Spinner:
                    return ControlType.Spinner;
                case AccessibleControlType.StatusBar:
                    return ControlType.StatusBar;
                case AccessibleControlType.Tab:
                    return ControlType.Tab;
                case AccessibleControlType.TabItem:
                    return ControlType.TabItem;
                case AccessibleControlType.Table:
                    return ControlType.Table;
                case AccessibleControlType.Text:
                    return ControlType.Text;
                case AccessibleControlType.TitleBar:
                    return ControlType.TitleBar;
                case AccessibleControlType.ToolBar:
                    return ControlType.ToolBar;
                case AccessibleControlType.ToolTip:
                    return ControlType.ToolTip;
                case AccessibleControlType.Tree:
                    return ControlType.Tree;
                case AccessibleControlType.TreeItem:
                    return ControlType.TreeItem;
                case AccessibleControlType.Window:
                    return ControlType.Window;
                case AccessibleControlType.Separator:
                    return ControlType.Separator;
                case AccessibleControlType.SemanticZoom:
                    return ControlType.SemanticZoom;
                case AccessibleControlType.Thumb:
                    return ControlType.Thumb;
                case AccessibleControlType.HeaderItem:
                    return ControlType.HeaderItem;
                case AccessibleControlType.Hyperlink:
                    return ControlType.Hyperlink;
                case AccessibleControlType.SplitButton:
                    return ControlType.SplitButton;
                case AccessibleControlType.DataItem:
                    return ControlType.DataItem;
                default:
                    return ControlType.Unknown;
            }
        });
    }
}