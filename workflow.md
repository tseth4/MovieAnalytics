# On your local machine
cd MovieAnalytics.Client
npm run build  # Outputs to ../MovieAnalytics.API/wwwroot

cd ../MovieAnalytics.API
dotnet publish -c Release -o publish

# On EC2
scp -i my-key.pem -r MovieAnalytics.API/publish ec2-user@<your-ec2-ip>:/home/ec2-user/app
ssh -i my-key.pem ec2-user@<your-ec2-ip>

# On EC2 instance
cd ~/app
nohup dotnet MovieAnalytics.API.dll > log.txt 2>&1 &
