using Microsoft.AspNetCore.Components.Web;

namespace Blazor.InputChips.Components.Shared;

public partial class InputChips : ComponentBase
{
	static readonly IEqualityComparer<string> StringEqualityComparer = StringComparer.OrdinalIgnoreCase;


	/// <summary>
	///		Current collection of chips/tags.
	/// </summary>
	[Parameter]
	public List<string> Chips { get; set; } = [];

	/// <summary>
	///		Callback invoked when <see cref="Chips"/> collection has changed.
	/// </summary>
	[Parameter]
	public EventCallback<List<string>> ChipsChanged { get; set; }


	/// <summary>
	///		A unique value identifying this isntance of the component.
	/// </summary>
	/// <remarks>
	///		Value is used in 'id' and 'name' attributes of elements.
	/// </remarks>
	[Parameter]
	public string InstanceId { get; set; } = Guid.NewGuid().ToString();


	/// <summary>
	///		Specifies a character (e.g. [SPACE]) that, like 
	///		an [ENTER] keypress, will submit the current value 
	///		to be added to the <see cref="Chips"/> collection.
	/// </summary>
	[Parameter]
	public char? SubmitChipOnChar { get; set; }

	/// <summary>
	///		Specifies whether to disallow (prevent) the [ENTER] key 
	///		to submit the value entered into the input field to be 
	///		submitted to be added to the <see cref="Chips"/> collection.
	/// </summary>
	/// <remarks>
	///		IMPORTANT:
	///		If this property is set to <c>True</c> and
	///		<see cref="SubmitChipOnChar"/> is null (has not been
	///		assigned a value), then the user will not be able to
	///		add values to the <see cref="Chips"/> collection.
	/// </remarks>
	[Parameter]
	public bool DisableSubmitChipOnEnter { get; set; }

	/// <summary>
	///		Specifies whether to allow duplicate entries to be 
	///		added to the <see cref="Chips"/> collection.
	/// </summary>
	[Parameter]
	public bool AllowDuplicates { get; set; }

	/// <summary>
	///		Specifies whether the <see cref="Chips"/> collection can be edited.
	/// </summary>
	[Parameter]
	public bool ReadOnly { get; set; }

	/// <summary>
	///		Specifies whether items in the readonly 
	///		chips collection can be deleted.
	/// </summary>
	[Parameter]
	public bool AllowDeleteWhenReadOnly { get; set; }

	/// <summary>
	///		Specifies whether the Backspace key will remove 
	///		the last chip in the <see cref="Chips"/> collection.
	/// </summary>
	/// <remarks>
	///		Applies only when the input field is empty.
	/// </remarks>
	[Parameter]
	public bool EnableBackspaceRemove { get; set; }

	/// <summary>
	///		Specifies whether to hide (not display) validation errors.
	/// </summary>
	[Parameter]
	public bool HideValidationErrors { get; set; }

	/// <summary>
	///		Specifies whether to apply the CSS class used 
	///		for styling the input field outline property.
	/// </summary>
	[Parameter]
	public bool ApplyInputOutlineCss { get; set; }

	/// <summary>
	///		Specifies whether to render an HTML unordered list 
	///		for displaying validation errors.
	/// </summary>
	/// <remarks>
	///		When <c>False</c> (default), a container &lt;div&gt; 
	///		is rendered, and a &lt;div&gt; for each error.
	///		
	///		When <c>True</c>, a &lt;ul&gt; is rendered as a container. 
	///		An &lt;li&gt; is then rendered for each error.
	///		
	///		If <see cref="ValidationErrorTemplate"/> is defined,
	///		it will be rendered for each error item within the container
	/// </remarks>
	[Parameter]
	public bool UseUnorderedListForErrors { get; set; }

	/// <summary>
	///		Specifies the font icon CSS class to use for the delete icon of a chip.
	/// </summary>
	/// <remarks>
	///		When a non-null value is specified for this property, 
	///		an &lt;i&gt; element is rendered instead of an &lt;img&gt;.
	/// </remarks>
	[Parameter]
	public string? DeleteIconFontCss { get; set; }

	/// <summary>
	///		Whitelist collection of allowed chip/tag values.
	/// </summary>
	[Parameter]
	public List<string>? AllowedChips { get; set; }

	/// <summary>
	///		The minimum permitted length of a chip value.
	/// </summary>
	[Parameter]
	public int MinChipLen { get; set; } = 1;

	/// <summary>
	///		The maximum permitted length of a chip value.
	/// </summary>
	[Parameter]
	public int MaxChipLen { get; set; } = int.MaxValue;

	/// <summary>
	///		Maximum number of chips allowed in the <see cref="Chips"/> collection.
	/// </summary>
	[Parameter]
	public int MaxNumChips { get; set; } = int.MaxValue;


	/// <summary>
	///		The error message to use when the chip/tag value 
	///		is not found in the <see cref="AllowedChips"/> collection.
	/// </summary>
	[Parameter]
	public string AllowedChipsErrorMessage { get; set; }
		= "Specified value not in list of allowed chip values.";

	/// <summary>
	///		The error message to use when the length of a chip value 
	///		is less than the value specified in <see cref="MinChipLen"/>.
	/// </summary>
	[Parameter]
	public string MinChipLenErrorMessage { get; set; }
		= "Specified chip value is shorter than the min length allowed.";

	/// <summary>
	///		The error message to use when the length of a chip value 
	///		exceeds the value specified in <see cref="MaxChipLen"/>.
	/// </summary>
	[Parameter]
	public string MaxChipLenErrorMessage { get; set; }
		= "Specified chip value exceeds the max length allowed.";

	/// <summary>
	///		The error message to use when the number of chips 
	///		in the <see cref="Chips"/> collection exceeds the value 
	///		specified in <see cref="MaxNumChips"/>.
	/// </summary>
	[Parameter]
	public string MaxNumChipsErrorMessage { get; set; }
		= "Adding this value will exceed the max number of chips allowed.";


	/// <summary>
	///		Callback invoked to performing custom validation on a new chip value.
	/// </summary>
	/// <remarks>
	///		The assigned callback is expected to add any validation errors 
	///		it generates to the <see cref="ChipValidationArgs.ValidationErrors"/> collection.
	/// </remarks>
	[Parameter]
	public EventCallback<ChipValidationArgs> OnCustomValidate { get; set; }


	/// <summary>
	///		Custom template to use for rendering an icon before 
	///		(to the left of) each rendered chip entry.
	/// </summary>
	[Parameter]
	public RenderFragment<string>? PrefixIconTemplate { get; set; }

	/// <summary>
	///		Custom template to use when displaying validation errors.
	/// </summary>
	[Parameter]
	public RenderFragment<string>? ValidationErrorTemplate { get; set; }


	/// <summary>
	///		Catch-All for all other attributes for the input.
	/// </summary>
	[Parameter(CaptureUnmatchedValues = true)]
	public Dictionary<string, object>? AllOtherAttributes { get; set; } = [];


	private string CssInputOutline =>
		this.ApplyInputOutlineCss ? "bic-input-outline" : string.Empty;

	private readonly Guid instanceId = Guid.NewGuid();
	private string? inputId;
	private string? inputName;
	private string? chipId;

	private string currentValue = string.Empty;
	private string previousValue = string.Empty;
	private List<string> validationErrors = [];


	protected override void OnInitialized()
	{
		base.OnInitialized();

		inputId = $"ic_{instanceId}";
		inputName = $"ic_{instanceId}";
		chipId = $"ic_{instanceId}";

		if (this.AllOtherAttributes is not null)
		{
			var idValue = this.AllOtherAttributes.TryGetValue("id", out var id) ? id.ToString() : null;
			var nameValue = this.AllOtherAttributes.TryGetValue("name", out var name) ? name.ToString() : null;

			if (idValue is not null)
			{
				inputId = idValue;
				inputName = idValue;
				chipId = idValue;
			}
			else if (nameValue is not null)
			{
				inputId = nameValue;
				inputName = nameValue;
				chipId = nameValue;
			}

			if (this.ReadOnly && !this.AllOtherAttributes.ContainsKey("readonly"))
			{
				this.AllOtherAttributes.Add("readonly", string.Empty);
			}
		}
	}


	private void OnInput(ChangeEventArgs args)
	{
		previousValue = currentValue;
		currentValue = args.Value?.ToString() ?? string.Empty;
	}

	private void OnKeyDown(KeyboardEventArgs args)
	{
		if (args.Key.IsBackspace() && (currentValue.Length == 0))
		{
			previousValue = currentValue;
		}
	}

	private void OnKeyUp(KeyboardEventArgs args)
	{
		if (this.EnableBackspaceRemove &&
			args.Key.IsBackspace() &&
			(!this.ReadOnly || this.AllowDeleteWhenReadOnly) &&
			(this.Chips.Count > 0) &&
			(previousValue.Length == 0))
		{
			RemoveChip(this.Chips.Last());
			currentValue = string.Empty;
			return;
		}

		if (this.ReadOnly ||
			!IsSubmitChipKeypress(args.Key) ||
			(this.Chips.Contains(currentValue, StringEqualityComparer) &&
			!this.AllowDuplicates))
		{
			return;
		}

		if (string.IsNullOrEmpty(currentValue)) return;
		if (!ValidateCurrentValue()) return;

		this.Chips.Add(currentValue);
		currentValue = string.Empty;
		this.ChipsChanged.InvokeAsync(this.Chips);
	}

	private void OnKeyPress(KeyboardEventArgs args)
	{
		if (IsSubmitChipKeypress(args.Key)) return;
		currentValue += args.Key;
	}


	private void RemoveChip(string chip)
	{
		this.Chips.Remove(chip);
		this.ChipsChanged.InvokeAsync(this.Chips);
	}

	private bool IsSubmitChipKeypress(string key) =>
		(!this.DisableSubmitChipOnEnter && key.IsEnterKey()) ||
		((this.SubmitChipOnChar is not null) && key.IsChar(this.SubmitChipOnChar.Value))
		;

	private bool ValidateCurrentValue()
	{
		validationErrors.Clear();

		if (this.Chips.Count >= this.MaxNumChips) validationErrors.Add(this.MaxNumChipsErrorMessage);
		if (currentValue.Length < this.MinChipLen) validationErrors.Add(this.MinChipLenErrorMessage);
		if (currentValue.Length > this.MaxChipLen) validationErrors.Add(this.MaxChipLenErrorMessage);
		if (((this.AllowedChips?.Count ?? 0) > 0) && !this.AllowedChips!.Contains(currentValue, StringEqualityComparer)) validationErrors.Add(this.AllowedChipsErrorMessage);
		if (this.OnCustomValidate.HasDelegate) this.OnCustomValidate.InvokeAsync(new ChipValidationArgs(this.Chips, currentValue, ref validationErrors));

		return (validationErrors.Count == 0);
	}
}

internal static class KeyValueExtensions
{
	const StringComparison StringCompareRules = StringComparison.OrdinalIgnoreCase;

	public static bool IsEnterKey(this string? key) => key?.Equals("Enter", StringCompareRules) ?? false;
	public static bool IsBackspace(this string? key) => key?.Equals("Enter", StringCompareRules) ?? false;
	public static bool IsChar(this string? key, char c) => key?.FirstOrDefault().Equals(c) ?? false;
}
