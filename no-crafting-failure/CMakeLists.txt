cmake_minimum_required(VERSION 3.8)

project(
    NoCraftingFailure
    VERSION 1.0.0
    LANGUAGES CSharp
)

include(CSharpUtilities)

add_library(${PROJECT_NAME} SHARED
    main.cs
)

set(_REFERENCES)
list(APPEND _REFERENCES
    "${BEPINEX_PATH}/BepInEx.dll"
    "${BEPINEX_PATH}/0Harmony.dll"
    "${RELEGEND_PATH}/ReLegend_Data/Managed/UnityEngine.dll"
    "${RELEGEND_PATH}/ReLegend_Data/Managed/UnityEngine.CoreModule.dll"
    "${RELEGEND_PATH}/ReLegend_Data/Managed/Assembly-CSharp.dll"
)
set(REFERENCES)
list(APPEND REFERENCES 
    "System" 
    "System.Core"
)

foreach(REF ${_REFERENCES})
    file(TO_NATIVE_PATH "${REF}" NREF)
    list(APPEND REFERENCES "${NREF}")
endforeach()

list(JOIN REFERENCES ";" REFS)

set_target_properties(${PROJECT_NAME} PROPERTIES
    VS_DOTNET_REFERENCES_COPY_LOCAL FALSE
    VS_DOTNET_REFERENCES "${REFS}"
    WIN32_EXECUTABLE FALSE
)
