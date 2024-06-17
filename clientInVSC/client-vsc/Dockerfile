FROM node:14-alpine

# Set the working directory in the container
WORKDIR /app/server

# Copy package.json and package-lock.json to the working directory
COPY server/package*.json ./

# Install dependencies
RUN npm install

# Copy the rest of the server application code to the working directory
COPY server/ .

# Expose the port that the server runs on
EXPOSE 3000

# Command to run the server
CMD ["npm", "start"]
