<template>
  <div>
    <div class="text-center">
      <h1 class="display-4">Temporary Image Hosting</h1>
      <p>Upload your images and share them for seven days. 32MB limit.</p>
    </div>

    <form @submit="handleSubmit">
      <div style="border: 1px dashed grey; padding: 30px">
        <div class="form-group">
          <label for="imageInput" class="d-block mb-2">Select an image</label>
          <input
            type="file"
            class="form-control-file"
            id="imageInput"
            accept="image/*"
          />
        </div>

        <button type="submit" class="btn btn-primary mt-4">Upload</button>
      </div>
    </form>
  </div>
</template>

<script>
import { BASE_API_URL } from "../constants";

export default {
  name: "Home",
  components: {
    // HelloWorld
  },
  data() {
    return {
      file: null,
    };
  },
  methods: {
    async handleSubmit(e) {
      e.preventDefault();

      const imageInput = document.querySelector("#imageInput");

      const file = imageInput.files[0];
      const fileName = file.name;

      // get secure url from our server
      const uploadUrl = await this.getUploadUrl(fileName);
      if (uploadUrl === null) return;

      // post the image direclty to the s3 bucket
      const uploadSuccess = await this.uploadImage(uploadUrl, file);
      if (!uploadSuccess) return;

      const imageUrl = uploadUrl.split("?")[0];

      this.$router.push({
        path: "success",
        query: {
          imageUrl: imageUrl,
        },
      });
    },

    async uploadImage(uploadUrl, file) {
      try {
        await fetch(uploadUrl, {
          method: "PUT",
          headers: {
            "Content-Type": "image/jpg",
          },
          body: file,
        });

        return true;
      } catch (err) {
        console.error(err);
        return false;
      }
    },

    async getUploadUrl(fileName) {
      try {
        return await fetch(`${BASE_API_URL}?fileName=${fileName}`).then((res) =>
          res.text()
        );
      } catch (err) {
        console.error(err);
        return null;
      }
    },
  },
};
</script>
