pipeline {
    agent any
    stages {
        stage('Build') {
            steps {
                echo 'Building the project...'
                // Hum container ke andar ka exact path use kar rahe hain
                sh '/usr/bin/docker-compose build'
            }
        }
        stage('Deploy') {
            steps {
                echo 'Deploying the project...'
                sh '/usr/bin/docker-compose up -d'
            }
        }
    }
}
