format:
	csharpier format .;

down:
	podman compose down;

up:
	make down;
	podman compose up -d --build;

shell:
	podman compose exec -u 1000:1000 timetable-generator-service bash;
