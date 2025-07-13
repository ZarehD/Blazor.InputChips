namespace Blazor.InputChips.Components;

public sealed class ChipValidationArgs(
	ICollection<string> allChips,
	string currentChip,
	ref List<string> validationErrors)
{
	/// <summary>
	///		Chip value being added.
	/// </summary>
	public string CurrentChip { get; } = currentChip;

	/// <summary>
	///		List of current chips.
	/// </summary>
	public IReadOnlyCollection<string> Chips { get; } = [.. allChips];

	/// <summary>
	///		Collection of validation errors.
	/// </summary>
	/// <remarks>
	///		Add custom validation errors to this collection.
	/// </remarks>
	public List<string> ValidationErrors { get; } = validationErrors;
}
