pipeline {
    agent any
    stages {
        stage('Build') {
            steps {
                // Is line se docker-compose ka path fix ho jayega
                sh '/usr/local/bin/docker-compose build'
            }
        }
        stage('Deploy') {
            steps {
                // Is line se aapka project start ho jayega
                sh '/usr/local/bin/docker-compose up -d'
            }
        }
    }
}
