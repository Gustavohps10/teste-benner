FROM node:18 AS build

WORKDIR /app

COPY microwave-benner.client/package*.json ./
RUN npm install

COPY microwave-benner.client/ ./
RUN npm run build

FROM nginx:alpine

COPY --from=build /app/dist /usr/share/nginx/html

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]
