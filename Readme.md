![Platform Support: ASP.NET Core](https://img.shields.io/static/v1?label=ASP.NET+Core&message=8.0%20-%209.0&color=blue&style=for-the-badge)
![License: Apache 2](https://img.shields.io/badge/license-Apache%202.0-blue?style=for-the-badge)

# <img src="projecticon.png" alt="project icon" width=40 /> Blazor InputChips Component

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
Property | Type | Default | Description
---|---|---|---
Chips | List<string> | new() | The collection of chip values to edit.<br/><br/>:bulb: <small>Supports binding (`@bind-Chips`).</small>
InstanceId | string | Guid.NewGuid() | A unique value identifying an isntance of the component.<br/><br/>:bulb: <small>Value is used in 'id' and 'name' attributes of elements.</small>

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
SubmitChipOnChar | char? | null | Specifies a character (e.g. [SPACE]) that, like an [ENTER] keypress, will submit the current value to be added to the `CharSubmitChip` collection.<br/><br/>:bulb:<small>Note that the [SPACE] character in the above example will not be a part of the submitted chip value.</small><br/><br/>:bulb:<small>Note also that the [ENTER] key will still submit a value regardless of this setting unless `DisableSubmitChipOnEnter` is `true`.</small>
MinChipLen | int | 1 | The minimum permitted length of a chip value.
MaxChipLen | int | int.MaxValue | The maximum permitted length of a chip value.
MaxNumChips | int | int.MaxValue | Maximum number of chips allowed in the `Chips` collection.

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
> Your custom validation callback (assigned to the `OnCustomValidate` callback) is expected to add any validation errors it generates to the `ChipValidationArgs.ValidationErrors` collection.


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
	--bic-container-border-color          : gray;
	--bic-container-border-width          : 1px;
	--bic-container-border-radius         : 0.25rem;
	--bic-chipslist-padding-x             : 0.125rem;
	--bic-chipslist-padding-y             : 0.125rem;
	--bic-chipitem-bgcolor                : silver;
	--bic-chipitem-color                  : black;
	--bic-chipitem-margin                 : 0.25rem;
	--bic-chipitem-padding-x              : 0.25rem;
	--bic-chipitem-padding-y              : 0;
	--bic-chipitem-height                 : 1.8rem;
	--bic-chipitem-line-height            : 1.5;
	--bic-chipitem-border-radius          : 1.25rem;
	--bic-chip-margin-x                   : 0.25rem;
	--bic-chip-margin-y                   : 0;
	--bic-chip-close-i-border-radius      : 50%;
	--bic-chip-close-i-margin-left        : 1px;
	--bic-chip-close-i-margin-top         : 1px;
	--bic-chip-close-i-padding            : 0 .25rem;
	--bic-chip-close-i-opacity            : .75;
	--bic-chip-close-i-hover-bgcolor      : gray;
	--bic-chip-close-i-hover-color        : white;
	--bic-chip-close-i-hover-opacity      : 1;
	--bic-chip-input-item-margin          : 0.25rem;
	--bic-chip-input-outline              : 1px solid lightblue;
	--bic-error-item-color                : red;
	--bic-error-item-fontsize             : 80%;
	--bic-error-item-margin-top           : 0.25rem;
}
```

### CSS Classes

Override or extend the style rules in one or more of the CSS classes.

```css
.bic-input-chips         # outer-most container
.bic-container           # inner container
.bic-chips-list          # chips list
.bic-chip-item           # chips list item container
.bic-chip                # chip value
.bic-chip-close          # chip close element (common styles)
img.bic-chip-close       # chip close img element (img element styles)
i.bic-chip-close         # chip close i element (i element styles)
.bic-chip-input-item     # chip input field container
input.bic-chip-input     # chip input field element (new chip entry field)
.bic-input-outline       # chip input field element outline styles
.bic-errors-container    # validation errors container
.bic-error-item          # validation error item
```


<br/>

## License
[Apache 2.0](https://github.com/ZarehD/RazorViewComponent/blob/master/LICENSE)


<br/>
⭐ If you like this project or find it useful, please give it a star. Thank you. ⭐
