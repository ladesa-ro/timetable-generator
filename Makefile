format:
	csharpier format .;

down:
	podman compose down;

up:
	make down;
	podman compose up -d --build;

shell:
	podman compose exec -u root timetable-generator-service bash;
