pipeline {
    agent any
    stages {
        stage('Build') {
            steps {
                echo 'Building the project...'
                // Hum direct command chala rahe hain, kyunki host par install ho chuka hai
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
