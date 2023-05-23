# Define the directory to search
$dir = "C:\Users\themis\source\repos\CFSH\templates\src\Infrastructure\Catalog\"

# Define the word to replace
$old_word = "Student"
$old_word_plural = "Students"

# Define the new word
$new_word = "{entityName}"
$new_word_plural = "{entityName}s"

# Iterate over each file in the directory and its subdirectories
Get-ChildItem -Path $dir -Recurse -File | ForEach-Object {
    # Read the file content
    $fileContent = Get-Content $_.FullName

    # Replace the old word with the new word in the content of the file
    $fileContent = $fileContent -ireplace $old_word_plural, $new_word_plural
    $fileContent = $fileContent -ireplace $old_word, $new_word

    # Write the new content back to the file
    Set-Content -Path $_.FullName -Value $fileContent

    # Replace the old word with the new word in the name of the file
    if ($_.Name -imatch $old_word_plural) {
        $newName = $_.Name -ireplace $old_word_plural, $new_word_plural
        Rename-Item -Path $_.FullName -NewName $newName
    } elseif ($_.Name -imatch $old_word) {
        $newName = $_.Name -ireplace $old_word, $new_word
        Rename-Item -Path $_.FullName -NewName $newName
    }
}
