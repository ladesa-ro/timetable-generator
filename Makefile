format:
	csharpier format .;

down:
	podman compose down;

up:
	podman compose up -d --build;

shell:
	podman compose exec -u 1000 timetable-generator bash;
