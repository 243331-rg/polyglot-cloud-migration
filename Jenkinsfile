pipeline {
    agent any
    stages {
        stage('Build') {
            steps {
                echo 'Building the project...'
                // Path hatakar sirf docker compose (plugin style) try karte hain
                sh 'docker compose build'
            }
        }
        stage('Deploy') {
            steps {
                echo 'Deploying the project...'
                sh 'docker compose up -d'
            }
        }
    }
}
