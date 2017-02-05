Framework "4.5.2x64"

properties {
	$base_dir = $psake.build_script_dir
    $tag
}

Task default -depends Clean, Build, Test

Task Build {
    write-host "Building"
    write-host $tag
}

Task Test {
}

Task Clean {
}