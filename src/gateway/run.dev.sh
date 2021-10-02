docker run --rm -it -p 9903:9903 -p 10000:10000  -v "./envoy.dev.yaml:/etc/envoy/envoy.yaml" --name envoy envoyproxy/envoy:v1.17.0
