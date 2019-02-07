
if [[ ! -d platform/unix ]]; then
	echo "ERROR: Can't find the 'plaform/unix' folder, make sure you run this from the root of the repository."
	exit 1
fi

# remove added code ...

SPLIT_FILE_LINE_KEY_WORD_X="SHELL_SPLIT_FILE_LINE_KEY_WORD_X_UUUUID_6NIN_9N_F9F9FB_EFN_F23FF2_43H340ADFGO34T0_ZZ"
add_code_to_file() {
    # target_cpp_file_h="xxx.h"
    # wrap_love_dll_file_h="wrap_love_dll (copy).h"

    target_cpp_file_h=$1
    wrap_love_dll_file_h=$2


    buf_str=$(grep -n ${SPLIT_FILE_LINE_KEY_WORD_X} ${target_cpp_file_h})
    if [ "$buf_str" ]; then
        echo "if yese"
        IFS=':' deleted_line_arr=($buf_str)
        deleted_line=${deleted_line_arr[0]}
        deleted_line_1=$(expr ${deleted_line} - 1)
        original_content=$(sed -n "1,${deleted_line_1}p" ${target_cpp_file_h})
        echo "${original_content}" > ${target_cpp_file_h}
    else
        echo "if no"
    fi

    echo "" >> "${target_cpp_file_h}"
    echo "// ${SPLIT_FILE_LINE_KEY_WORD_X}" >> "${target_cpp_file_h}"
    cat "${wrap_love_dll_file_h}" >> "${target_cpp_file_h}"
}

# download files
wget https://github.com/endlesstravel/Love2dCS/raw/master/c_api_src/wrap_love_dll.cpp -o src/modules/wrap_love_dll.cpp
wget https://github.com/endlesstravel/Love2dCS/raw/master/c_api_src/wrap_love_dll.h -o src/modules/wrap_love_dll.h

# rm '#include "wrap_love_dll.h"' instructions
sed -e '/#include "wrap_love_dll.h"/d'  "src/modules/wrap_love_dll.cpp"  > "src/modules/wrap_love_dll.cpp.tmp"
cat "src/modules/wrap_love_dll.cpp.tmp"  > "src/modules/wrap_love_dll.cpp"
rm "src/modules/wrap_love_dll.cpp.tmp"

# output to target file
add_code_to_file "src/modules/love.h" "src/modules/wrap_love_dll.h"
add_code_to_file "src/modules/love.cpp" "src/modules/wrap_love_dll.cpp"