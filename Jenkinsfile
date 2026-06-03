pipeline {
    agent any
    stages {
        stage('Build') {
            steps {
                echo 'Building the project...'
                // Humne yahan full path use kiya hai taaki command mil jaye
                sh '/usr/local/bin/docker-compose build'
            }
        }
        stage('Deploy') {
            steps {
                echo 'Deploying the project...'
                sh '/usr/local/bin/docker-compose up -d'
            }
        }
    }
}
