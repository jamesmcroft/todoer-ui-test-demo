# ToDoer App

## Running the app

> We use [mkcert](https://github.com/FiloSottile/mkcert) to generate locally
> trusted development certificates, this is required to be able to run
> `localhost` over https. Which is also more secure than exposing your
> connection through a tunneling service like `ngrok` or `localtunnel`.
>
> When you run for the first time, you will be prompted to enter
> your password so `mkcert` can generate the certificate for you.
>
> You can check the generated certificate inside this path
> `~/.vite-plugin-mkcert`

From the src folder,

- Run `npm install` to install dependencies
- Run `npm run dev` to start developing, you should have a URL that looks like this

```
https://localhost:3000
```

## Building the app

From the src folder, run `npm run build` and this will generate a static output
inside `dist/` which you can host on static hosting service.
