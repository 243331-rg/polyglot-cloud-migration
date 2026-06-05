pipeline {
    agent any
    environment {
        // Ye line Docker API version ka conflict solve kar degi
        DOCKER_API_VERSION = '1.44'
    }
    stages {
        stage('Build') {
            steps {
                // Yahan full path use karein
                sh '/usr/local/bin/docker-compose build'
            }
        }
        stage('Deploy') {
            steps {
                sh '/usr/local/bin/docker-compose up -d'
            }
        }
    }
}
