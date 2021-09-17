# npm
FROM node:lts-alpine as npm-stage
WORKDIR /app
COPY package*.json ./
RUN npm install


# build stage
FROM npm-stage as build-stage
COPY . .
RUN npm run build

# build stage
FROM npm-stage as develop
EXPOSE 8080
CMD ["npm", "run", "serve"]


# production stage
FROM nginx:stable-alpine as production-stage
COPY --from=build-stage /app/dist /usr/share/nginx/html
COPY default.conf /etc/nginx/conf.d/
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]