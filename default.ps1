Framework "4.5.2x64"

properties {
	$base_dir = $psake.build_script_dir
    $build_number = $psake.build_number
}

Task default -depends Clean, Build, Test

Task Build {
    write-host "Building"
    write-host $build_number
}

Task Test {
}

Task Clean {
}