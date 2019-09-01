cmake_minimum_required(VERSION 3.8)

project(
    ReLegendMods
    VERSION 0.0.0
)

# Set up for Re:Legend modding.
set(CMAKE_DOTNET_TARGET_FRAMEWORK_VERSION "v4.0")

# Options
set(RELEGEND_PATH "C:/Program Files (x86)/Steam/steamapps/common/Re-Legend" CACHE PATH "Path to Re:Legend install.")
set(BEPINEX_PATH "${CMAKE_CURRENT_SOURCE_DIR}/thirdparty/bepinex")

add_subdirectory(attack-and-move)
add_subdirectory(no-crafting-failure)
add_subdirectory(tutorial-skipper)
