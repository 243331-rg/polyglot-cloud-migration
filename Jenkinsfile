pipeline {
    agent any
    stages {
        stage('Build') {
            steps {
                script {
                    // Hum check karenge ki kaunsa command kaam kar raha hai
                    def dockerComposeCmd = ""
                    try {
                        sh 'docker-compose --version'
                        dockerComposeCmd = 'docker-compose'
                    } catch (Exception e) {
                        try {
                            sh 'docker compose version'
                            dockerComposeCmd = 'docker compose'
                        } catch (Exception e2) {
                            error "Docker Compose nahi mila! Check karein ki Docker-Compose install hai ya nahi."
                        }
                    }
                    
                    echo "Using command: ${dockerComposeCmd}"
                    sh "${dockerComposeCmd} build"
                }
            }
        }
        stage('Deploy') {
            steps {
                script {
                    sh 'docker-compose up -d' // Yahan aap apni pehchan ki command likhein
                }
            }
        }
    }
}
