Framework "4.5.2x64"

properties {
	$base_dir = $psake.build_script_dir
}

Task default -depends Clean, Build, Test

Task Build {
}

Task Test {
}

Task Clean {
}