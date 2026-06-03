pipeline {
    agent any
    stages {
        stage('Build') {
            steps {
                echo 'Building the project...'
                // Hum host ke docker-compose ko map karke use karenge
                sh 'docker-compose build'
            }
        }
        stage('Deploy') {
            steps {
                echo 'Deploying the project...'
                sh 'docker-compose up -d'
            }
        }
    }
}
