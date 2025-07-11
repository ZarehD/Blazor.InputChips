![Platform Support: ASP.NET Core](https://img.shields.io/static/v1?label=ASP.NET+Core&message=8.0%20-%209.0&color=blue&style=for-the-badge)
![License: Apache 2](https://img.shields.io/badge/license-Apache%202.0-blue?style=for-the-badge)

# <img src="https://github.com/ZarehD/Blazor.InputChips/blob/main/ProjectIcon.png" alt="project icon" width=40 /> Blazor InputChips Component

Input control for editing a collection of chips (tag values).


## Installation

1. Install the nuget package.

   ```xml
   <PackageReference Include="Blazor.InputChips" Version="1.0.0" />
   ```

1. Import the component stylesheet

   ```html
   <head>
     <link rel="stylesheet" href="_content/Blazor.InputChips/Blazor.InputChips.styles.min.css" />
     <link rel="stylesheet" href="site.min.css" />
   </head>
   ```

1. Update __imports.razor_

   ```razor
   @using Blazor.InputChips.Components.Shared
   ```

1. Use the component

   ```C#
   <InputChips @bind-Chips="this.Chips"
               placeholder="add a tag"
               MinChipLen="3" MaxChipLen="30" />
   
   @code {
       public List<string> Chips { get; set; } = [];
   }
   ```


## Configure Functionality

### Properties

#### Binding Value & Instance ID
Property | Type | Description
---|---|---
Chips | List\<string\> | The collection of chip values to edit.<br/><br/>:bulb: <small>Supports binding (`@bind-Chips`).</small>
InstanceId | string | A unique value identifying an instance of the component.<br/><br/>:bulb: <small>Value is used in 'id' and 'name' attributes of elements.</small><br/>:bulb: <small>`Guid.NewGuid().ToString()` is used if no value assigned.</small>

#### Flags/Switches
Property | Type | Default | Description
---|---|---|---
AllowDeleteWhenReadOnly | bool | false | Specifies whether items in the `Chips` collection can be deleted when the `ReadOnly` property is `true`.
AllowDuplicates | bool | false | Specifies whether to allow duplicate chip entries.<br/><br/>:bulb:<small>Note that when set to `false`, duplicate chip values will not trigger an error. Instead, the value will remain in the input field and will not be added to the `Chips` collection.</small>
ApplyInputOutlineCss | bool | false | Specifies whether to apply the CSS class used for styling the input field outline property (`.bic-input-outline`).
DisableSubmitChipOnEnter | bool | false | Disables the [ENTER] key from submitting a value to be added to the `Chips` collection.<br/><br/>:bulb:<small>Note that if this property is set to `true` and the `SubmitChipOnChar` property is null, values entered in the input field will not be added to the `Chips` collection.</small>
EnableBackspaceRemove | bool | false | Specifies whether the [BACKSPACE] key will remove the last chip in the `Chips` collection.<br/><br/>:bulb: <small>Applies only when the input field is empty.</small>
HideValidationErrors | bool | false | Specifies whether to hide (not display) validation errors.
ReadOnly | bool | false | Specifies whether the `Chips` collection can be edited.

#### Miscellaneous
Property | Type | Default | Description
---|---|---|---
AllowedChips | List\<string\>? | null | Whitelist collection of permitted chip/tag values.
DeleteIconFontCss | string? | null | Specifies a CSS class used for the delete icon of a chip.<br/><br/>:bulb: <small>When a non-null value is specified for this property, an `<i class="bic-chip-close @DeleteIconFontCss">` element is rendered instead of `<img class="bic-chip-close" ... />`.</small>
MinChipLen | int | 1 | The minimum permitted length of a chip value.
MaxChipLen | int | int.MaxValue | The maximum permitted length of a chip value.
MaxNumChips | int | int.MaxValue | Maximum number of chips allowed in the `Chips` collection.
SubmitChipOnChar | char? | null | Specifies a character (e.g. [SPACE]) that, like an [ENTER] keypress, will submit the current value to be added to the `CharSubmitChip` collection.<br/><br/>:bulb:<small>Note that the [SPACE] character in the above example will not be a part of the submitted chip value.</small><br/><br/>:bulb:<small>Note also that the [ENTER] key will still submit a value regardless of this setting unless `DisableSubmitChipOnEnter` is `true`.</small>

#### Error Messages
Property | Type | Description
---|---|---
AllowedChipsErrorMessage | string | The error message to use when the specified chip/tag value is not in the `AllowedChips` collection.
MinChipLenErrorMessage | string | The error message to use when the length of a chip value is less than the value specified in `MinChipLen`.
MaxChipLenErrorMessage | string | The error message to use when the length of a chip value exceeds the value specified in `MaxChipLen`.
MaxNumChipsErrorMessage | string | The error message to use when the attempting to add a chip to the `Chips` collection would exceed the value specified in `MaxNumChips`.


### Event Callbacks

Callback | Type | Description
---|---|---
ChipsChanged | EventCallback<List\<string\>> | Callback invoked when Chips collection is modified.
OnCustomValidate | EventCallback\<ChipValidationArgs\> | Callback invoked to performing custom validation on a new chip value.

> [!Important]
> Your custom validation callback (assigned to the `OnCustomValidate` property) is expected to add any validation errors it generates to the `ChipValidationArgs.ValidationErrors` collection.


### Child Content Templates

Template | Description
---|---
PrefixIconTemplate | Custom template to use for rendering an icon before (to the left of) each rendered chip entry.
ValidationErrorTemplate | Custom template to use when displaying validation errors.

Template usage:

```C#
<InputChips ...>
  <PrefixIconTemplate>
    <img src="() => GetImageForChipValue(chip)">
  </PrefixIconTemplate>
  <ValidationErrorTemplate>
    <div>
      @error
    </div>
  </ValidationErrorTemplate>
</InputChips>
```


## Modify Styling

You can style the component in two ways.

### CSS Vars

Override one of the available CSS variables by assigning it a new value (in your own CSS file).

```css
:root {
  --bic-container-border-color
  --bic-container-border-width
  --bic-container-border-radius
  --bic-chipslist-padding-x
  --bic-chipslist-padding-y
  --bic-chipitem-bgcolor
  --bic-chipitem-color
  --bic-chipitem-margin
  --bic-chipitem-padding-x
  --bic-chipitem-padding-y
  --bic-chipitem-height
  --bic-chipitem-line-height
  --bic-chipitem-border-radius
  --bic-chip-margin-x
  --bic-chip-margin-y
  --bic-chip-close-i-border-radius
  --bic-chip-close-i-margin-left
  --bic-chip-close-i-margin-top
  --bic-chip-close-i-padding
  --bic-chip-close-i-opacity
  --bic-chip-close-i-hover-bgcolor
  --bic-chip-close-i-hover-color
  --bic-chip-close-i-hover-opacity
  --bic-chip-input-item-margin
  --bic-chip-input-outline
  --bic-error-container-ul-list-style
  --bic-error-container-ul-padding-left
  --bic-error-container-ul-margin-bottom
  --bic-error-item-color
  --bic-error-item-fontsize
  --bic-error-item-margin-top
}
```

### CSS Classes

Override or extend the style rules in one or more of the CSS classes.

```css    
.bic-input-chips             # outer-most container
.bic-container               # inner container
.bic-chips-list              # chips list
.bic-chip-item               # chips list item container
.bic-chip                    # chip value
.bic-chip-close              # chip close element (common styles)
img.bic-chip-close           # chip close img element (img element styles)
i.bic-chip-close             # chip close i element (i element styles)
.bic-chip-input-item         # chip input field container
input.bic-chip-input         # chip input field element (new chip entry field)
.bic-input-outline           # chip input field element outline styles
div.bic-errors-container     # validation errors div container
div.bic-error-item           # validation error div item
ul.bic-errors-container      # validation errors ul container
li.bic-error-item            # validation error li item
```


<br/>

## License
[Apache 2.0](https://github.com/ZarehD/Blazor.InputChips/blob/main/LICENSE)


<br/>
⭐ If you like this project or find it useful, please give it a star. Thank you. ⭐
