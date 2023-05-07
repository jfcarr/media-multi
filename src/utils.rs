use std::env;
use std::path::Path;

/**
 * Given a file name, return the file's extension without the leading period.
 *
 * Example: input of 'test1.avi' returns 'avi'.
 */
pub fn extract_file_stem(input_file: &String) -> String {
    let file_stem = Path::new(input_file.as_str())
        .file_stem()
        .unwrap()
        .to_os_string()
        .into_string()
        .unwrap();

    return file_stem;
}

/**
 * Given a file name, walk the environment PATH to see if it's available.
 */
pub fn is_in_path(file_name: &str) -> bool {
    let path_string: String = env::var("PATH").unwrap().to_string();

    let split_string: std::str::Split<char> = path_string.split(':');

    let mut file_available: bool = false;
    for path_segment in split_string {
        let full_path: std::path::PathBuf = Path::new(path_segment).join(file_name);

        if Path::new(full_path.display().to_string().as_str()).exists() {
            file_available = true;

            break;
        }
    }

    return file_available;
}
